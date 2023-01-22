using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
      //Services instance , Dependancy injection 
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //from services.
            var genres = await _genresService.GetAll();
           
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]  GenreDto dto) //or add also[Frombody] as a confirmation  //object of the entity
        {
            // 1 - get the data from user (object) -DTo-
            var genre = new Genre { Name = dto.Name }; //oject initilize from user
                                                       //= Genre genre = new (){} ;

            // Check if already exsist 
            var genre_Exist = await _genresService.Is_Name_Exsist(dto.Name);
            if (genre_Exist == true)
                return NotFound("This Genre is alredy exsist");


            // 2- Pass this object to services
            await _genresService.Add(genre); // Add(Entity_object)
            //=  await _genresService.Geners.Add(genre); //Old syntax

            return Ok(genre); // why retern the object ? to be avilable for use in needed ex : createdon
        }

        [HttpPut("{id}")] // templete
        public async Task<IActionResult> UpdateAsync(byte id, [FromForm] [FromBody] GenreDto dto)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null)
                return NotFound($"No genre was found with ID: {id}");

            genre.Name = dto.Name;

            _genresService.Update(genre);

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null)
                return NotFound($"No genre was found with ID: {id}");

            _genresService.Delete(genre);

            return Ok(genre);
        }

    }
}