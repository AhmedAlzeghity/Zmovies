using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Services
{
    // Layer from and to the API -Between Api and Db-
    public class GenresService : IGenresService
    {
        // Db connection , Dependancy injection
        
        private readonly ApplicationDbContext _context;

        public GenresService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add new object "Genre"
        public async Task<Genre> Add(Genre genre) // DB Object Genre from end user
        {

            await _context.AddAsync(genre);   // Add this object
            _context.SaveChanges();           // Save to db

            return genre;
        }

        // No need to make async
        public Genre Delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            // return await _context.Genres.ToListAsync(); without otrdering (order by primary key as a default)
        }

        public async Task<Genre> GetById(byte id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public Task<bool> IsvalidGenre(byte id)
        {
            return _context.Genres.AnyAsync(g => g.Id == id); //Any from EF : check any 
        }
        public Task<bool> Is_Name_Exsist(string GenreName)
        {

            //return 
            //  var isExsist= _context.Genres.SingleOrDefaultAsync(g => g.Name == GenreName);
           return _context.Genres.AnyAsync(g => g.Name == GenreName);
            int x = 0;
            // Any () : return true if at least one exsist
            
        }

        // No need to make async
        public Genre Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();

            return genre;
        }
    }
}
