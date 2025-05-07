using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firma.Intranet.Data;
using Firma.Intranet.Models;

namespace Firma.Intranet.Controllers
{
    public class HorsesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HorsesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Horses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Horses.ToListAsync());
        }

        // GET: Horses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var horse = await _context.Horses.FirstOrDefaultAsync(m => m.Id == id);
            if (horse == null)
                return NotFound();

            return View(horse);
        }

        // GET: Horses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Horses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Horse horse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horse);
                await _context.SaveChangesAsync();
                return RedirectToAction("Horses", "Home");
            }
            return View(horse);
        }

        // GET: Horses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var horse = await _context.Horses.FindAsync(id);
            if (horse == null)
                return NotFound();

            return View(horse);
        }

        // POST: Horses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Breed,Age,Status,LastCheckup")] Horse horse)
        {
            if (id != horse.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorseExists(horse.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Horses", "Home");
            }
            return View(horse);
        }

        // GET: Horses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var horse = await _context.Horses.FirstOrDefaultAsync(m => m.Id == id);
            if (horse == null)
                return NotFound();

            return View(horse);
        }

        // POST: Horses/DeleteConfirmed (modal)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horse = await _context.Horses.FindAsync(id);
            if (horse != null)
            {
                _context.Horses.Remove(horse);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Horses", "Home");
        }

        private bool HorseExists(int id)
        {
            return _context.Horses.Any(e => e.Id == id);
        }
    }
}
