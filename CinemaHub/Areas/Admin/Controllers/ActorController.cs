using CinemaBooking.Data;
using CinemaHub.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaHub.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ActorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ActorController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _db.Actors
               .Include(c => c.Movies)
               .ToListAsync();

            return View(movies);
        }



        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            _db.Actors.Add(actor);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _db.Actors
                .FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            try
            {
                _db.Actors.Update(actor);

                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(actor.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _db.Actors.Any(a => a.Id == id);
        }






        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _db.Actors.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            _db.Actors.Remove(actor);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




    }

}
