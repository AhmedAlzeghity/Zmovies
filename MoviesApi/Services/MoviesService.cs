using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ApplicationDbContext _context;

        public MoviesService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Movie> Add(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();

            return movie;
        }

        public Movie Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();

            return movie;
        }

        // Get all in selected genre or all 
        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0) // if no genreId
        {
            return await _context.Movies
                // Instead of two methods for getAll and getById ;
                // we can use same method and handle the parameter if 0 > get all 
                
                .Where(m => m.GenreId == genreId || genreId == 0) // if genreId =0 > continue and getall 
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Genre)         // Include navigation properity , return as object , handeled in front end
                        // if not using auto mapper > 
                        // .select (m=> new MovieDeatailsDto 
                        // {id =m.id , generid =m.Generid,Rate = m.Rate})
                
                .ToListAsync();
        }

        public async Task<Movie> GetById(int id) 
        {
            return await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
           
            // If not use Mapper
            //in the controller
            // var DTO = new movie {
            //  ID =movie.id , DTO_Prop = Db_Entity_Prop }

        }

        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();

            return movie;
        }
    }
}