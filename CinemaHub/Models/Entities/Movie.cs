namespace CinemaHub.Models.Entities
{

    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public MovieStatus Status { get; set; }

        public DateTime DateTime { get; set; }

        public string? MainImage { get; set; }


        // =========================
        // Category
        // =========================

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;


        // =========================
        // Cinema
        // =========================

        public int CinemaId { get; set; }

        public Cinema Cinema { get; set; } = null!;


        // =========================
        // Actors
        // =========================

        public ICollection<Actor> Actors { get; set; } = new List<Actor>();


        // =========================
        // Sub Images
        // =========================

        public ICollection<MovieImage> Images { get; set; } = new List<MovieImage>();
    }

}