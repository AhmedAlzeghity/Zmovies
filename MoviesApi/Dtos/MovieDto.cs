namespace MoviesApi.Dtos
{
    public class MovieDto
    {
        // No need for sending ID as it identity 
        [MaxLength(250)]

        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        [MaxLength(2500)]
        public string Storeline { get; set; }

       
        // This for file types > Appear : Upload button
        // Adding files : 1- select the accebtable excetintions 2- the max size 
        public IFormFile? Poster { get; set; }

        public byte GenreId { get; set; }
    }
}