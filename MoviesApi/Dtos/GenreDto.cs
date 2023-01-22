namespace MoviesApi.Dtos
{
    public class GenreDto
    {
        // Id already identity (no need to transfer)

        // Not more db capacity
        [MaxLength(100)] //Already required > nullable : disabled
        public string Name { get; set; }
    }
}