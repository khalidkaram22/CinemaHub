using CinemaHub.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<MovieImage> MovieImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Movie -> Category
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Category)
                .WithMany(c => c.Movies)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            // Movie -> Cinema
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Cinema)
                .WithMany(c => c.Movies)
                .HasForeignKey(m => m.CinemaId)
                .OnDelete(DeleteBehavior.Restrict);


            // Movie -> MovieImages
            modelBuilder.Entity<MovieImage>()
                .HasOne(mi => mi.Movie)
                .WithMany(m => m.Images)
                .HasForeignKey(mi => mi.MovieId)
                .OnDelete(DeleteBehavior.Cascade);


            // Movie <-> Actor
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Actors)
                .WithMany(a => a.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieActor",
                    j => j
                        .HasOne<Actor>()
                        .WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade),

                    j => j
                        .HasOne<Movie>()
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade),

                    j =>
                    {
                        j.HasKey("MovieId", "ActorId");
                    });
        }
    }
}