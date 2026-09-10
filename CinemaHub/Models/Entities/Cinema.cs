namespace CinemaHub.Models.Entities
{
    public class Cinema
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        // Navigation Property
       public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}