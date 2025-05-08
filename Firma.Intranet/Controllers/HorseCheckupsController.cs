using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firma.Intranet.Data;
using Firma.Intranet.Models;

namespace Firma.Intranet.Controllers
{
    public class HorseCheckupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HorseCheckupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HorseCheckups
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HorseCheckups.Include(h => h.Horse);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HorseCheckups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horseCheckup = await _context.HorseCheckups
                .Include(h => h.Horse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horseCheckup == null)
            {
                return NotFound();
            }

            return View(horseCheckup);
        }

        // GET: HorseCheckups/Create
        public IActionResult Create()
        {
            ViewData["HorseId"] = new SelectList(_context.Horses, "Id", "Name");
            return View();
        }

        // POST: HorseCheckups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HorseCheckup horseCheckup)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.HorseId = new SelectList(_context.Horses, "Id", "Name", horseCheckup.HorseId);
                return View(horseCheckup);
            }

            // Dodaj przegląd
            _context.HorseCheckups.Add(horseCheckup);

            // Zaktualizuj LastCheckup
            var horse = await _context.Horses.FindAsync(horseCheckup.HorseId);
            if (horse != null)
            {
                horse.LastCheckup = horseCheckup.CheckupDate;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: HorseCheckups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horseCheckup = await _context.HorseCheckups.FindAsync(id);
            if (horseCheckup == null)
            {
                return NotFound();
            }
            ViewData["HorseId"] = new SelectList(_context.Horses, "Id", "Name", horseCheckup.HorseId);
            return View(horseCheckup);
        }

        // POST: HorseCheckups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HorseId,CheckupDate,Type,Notes")] HorseCheckup horseCheckup)
        {
            if (id != horseCheckup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horseCheckup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorseCheckupExists(horseCheckup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HorseId"] = new SelectList(_context.Horses, "Id", "Name", horseCheckup.HorseId);
            return View(horseCheckup);
        }

        // GET: HorseCheckups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horseCheckup = await _context.HorseCheckups
                .Include(h => h.Horse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horseCheckup == null)
            {
                return NotFound();
            }

            return View(horseCheckup);
        }

        // POST: HorseCheckups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horseCheckup = await _context.HorseCheckups.FindAsync(id);
            if (horseCheckup != null)
            {
                _context.HorseCheckups.Remove(horseCheckup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorseCheckupExists(int id)
        {
            return _context.HorseCheckups.Any(e => e.Id == id);
        }
    }
}
