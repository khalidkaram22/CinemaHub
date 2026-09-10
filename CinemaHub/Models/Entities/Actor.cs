namespace CinemaHub.Models.Entities
{
    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        // Many-to-Many
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}