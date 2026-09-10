namespace CinemaHub.Models.Entities
{
    public class MovieImage
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        // FK
        public int MovieId { get; set; }

        // Navigation
        public Movie Movie { get; set; } = null!;
    }
}