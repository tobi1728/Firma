using System;
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
            var checkups = _context.HorseCheckups.Include(h => h.Horse);
            return View(await checkups.ToListAsync());
        }

        // GET: HorseCheckups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var horseCheckup = await _context.HorseCheckups
                .Include(h => h.Horse)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (horseCheckup == null) return NotFound();

            return View(horseCheckup);
        }

        // GET: HorseCheckups/Create
        public IActionResult Create()
        {
            ViewData["HorseId"] = new SelectList(_context.Horses, "Id", "Name");
            return View();
        }

        // POST: HorseCheckups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HorseCheckup horseCheckup)
        {
            if (!ModelState.IsValid)
            {
                ViewData["HorseId"] = new SelectList(_context.Horses, "Id", "Name", horseCheckup.HorseId);
                return View(horseCheckup);
            }

            _context.Add(horseCheckup);
            await _context.SaveChangesAsync();

            await UpdateLastCheckupAsync(horseCheckup.HorseId);
            return RedirectToAction(nameof(Index));
        }

        // GET: HorseCheckups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var horseCheckup = await _context.HorseCheckups.FindAsync(id);
            if (horseCheckup == null) return NotFound();

            ViewData["HorseId"] = new SelectList(_context.Horses, "Id", "Name", horseCheckup.HorseId);
            return View(horseCheckup);
        }

        // POST: HorseCheckups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HorseCheckup horseCheckup)
        {
            if (id != horseCheckup.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["HorseId"] = new SelectList(_context.Horses, "Id", "Name", horseCheckup.HorseId);
                return View(horseCheckup);
            }

            try
            {
                _context.Update(horseCheckup);
                await _context.SaveChangesAsync();

                await UpdateLastCheckupAsync(horseCheckup.HorseId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorseCheckupExists(horseCheckup.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: HorseCheckups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var horseCheckup = await _context.HorseCheckups
                .Include(h => h.Horse)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (horseCheckup == null) return NotFound();

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
                int horseId = horseCheckup.HorseId;

                _context.HorseCheckups.Remove(horseCheckup);
                await _context.SaveChangesAsync();

                await UpdateLastCheckupAsync(horseId);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task UpdateLastCheckupAsync(int horseId)
        {
            var latest = await _context.HorseCheckups
                .Where(c => c.HorseId == horseId)
                .OrderByDescending(c => c.CheckupDate)
                .FirstOrDefaultAsync();

            var horse = await _context.Horses.FindAsync(horseId);
            if (horse != null)
            {
                horse.LastCheckup = latest?.CheckupDate;
                await _context.SaveChangesAsync();
            }
        }

        private bool HorseCheckupExists(int id)
        {
            return _context.HorseCheckups.Any(e => e.Id == id);
        }
    }
}
