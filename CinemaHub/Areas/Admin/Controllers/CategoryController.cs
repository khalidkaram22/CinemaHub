using CinemaBooking.Data;
using CinemaHub.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _db.Categories.ToListAsync();

            return View(categories);
        }

       
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _db.Categories.Add(category);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _db.Categories
                .FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            try
            {
                _db.Categories.Update(category);

                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _db.Categories.Any(c => c.Id == id);
        }






        [HttpPost]
        
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _db.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}
