using CinemaBooking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MovieController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _db.Movies
                .Include(m => m.Category)
                .Include(m => m.Cinema)
                .Include(m => m.Actors)
                .ToListAsync();

            return View(movies);
        }
    }
}
