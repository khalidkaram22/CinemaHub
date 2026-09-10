namespace CinemaHub.Models.Entities
{
   
        public class Category
        {
            public int Id { get; set; }

            public string Name { get; set; } = string.Empty;

            // Navigation Property
            public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        }
    
}
