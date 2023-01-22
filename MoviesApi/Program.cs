using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoviesApi.Models;
using MoviesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//////////////// Add services 
builder.Services.AddTransient<IGenresService, GenresService>();
builder.Services.AddTransient<IMoviesService, MoviesService>();

builder.Services.AddAutoMapper(typeof(Program));  // my program.cs > will scan all the project and deal with any use 

builder.Services.AddCors(); // To allow access from other ports 
    
//Add options to sagger 
builder.Services.AddSwaggerGen(options =>
{
    // Add metadata and more 
    options.SwaggerDoc("v1", new OpenApiInfo 
    {
        Version = "v1",
        Title = "MoviesApi",
        Description = "My first api",
        TermsOfService = new Uri("https://www.google.com"),
        // Open api
        Contact = new OpenApiContact 
        {
            Name = "DevCreed",
            Email = "test@domain.com",
            Url = new Uri("https://www.google.com")
        },
        License = new OpenApiLicense
        {
            Name = "My license",
            Url = new Uri("https://www.google.com")
        }
    });
    // options.SwaggerDoc("v2", new OpenApiInfo()); // Other Documentation

    
    //  Add Authontication , Tocken in the request's header or ..
    //  General Authorization for all 
    
    //  OpenApiSecurityScheme  initiization
    //  options.AddSecurityDefinition("Name" , Object Securityschema);
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",     // Format
        In = ParameterLocation.Header,  // Where 
        // Description which appear in textbox 4 the user
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
    });

    // Specific Secuirity for every endpoint
    options.AddSecurityRequirement(new OpenApiSecurityRequirement 
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
// Only in Dev environment 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Allow any header > Allow any method > Allow orgin(URL )
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); //Allow cors :  Step 2 

app.UseAuthorization();

app.MapControllers();

app.Run();
