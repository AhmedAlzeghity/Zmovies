namespace MoviesApi.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [MaxLength(250)]

        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        [MaxLength(2500)]
        public string Storeline { get; set; }

        // save images as array bytes , Prefere to integrate with service > send pic and recive url
        public byte[] Poster { get; set; }


        // Same type of other entity PK
        // As it same name of the other entity and there is navigation properity > EF will consider this as FK
        // if not same name > Must add annotation         
        public byte GenreId { get; set; } // FK

        //Navigation properity
        public Genre Genre { get; set; }
    }
}