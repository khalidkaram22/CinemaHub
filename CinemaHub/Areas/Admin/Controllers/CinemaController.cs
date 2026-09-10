using CinemaBooking.Data;
using CinemaHub.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CinemaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CinemaController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var cinemas = await _db.Cinemas
                .Include(c => c.Movies)
                .ToListAsync();

            return View(cinemas);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }

            _db.Cinemas.Add(cinema);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> Edit(int id)
        {
            var Cinema = await _db.Cinemas
                .FindAsync(id);

            if (Cinema == null)
            {
                return NotFound();
            }

            return View(Cinema);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema Cinema)
        {
            if (id != Cinema.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(Cinema);
            }

            try
            {
                _db.Cinemas.Update(Cinema);

                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinemaExists(Cinema.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CinemaExists(int id)
        {
            return _db.Cinemas.Any(a => a.Id == id);
        }


        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var Cinema = await _db.Cinemas.FindAsync(id);

            if (Cinema == null)
            {
                return NotFound();
            }

            _db.Cinemas.Remove(Cinema);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }







    }
}
