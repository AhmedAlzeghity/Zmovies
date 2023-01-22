namespace MoviesApi.Dtos
{
    public class MovieDetailsDto
    {
        // nO need for data annotation
        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public string Storeline { get; set; }

        public byte[] Poster { get; set; }

        public byte GenreId { get; set; }

        //only need genere name
        public string GenreName { get; set; }
    }
}