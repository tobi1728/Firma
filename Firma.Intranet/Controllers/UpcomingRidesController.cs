using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firma.Intranet.Data;
using Firma.Intranet.Models;

namespace Firma.Intranet.Controllers
{
    public class UpcomingRidesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UpcomingRidesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var rides = _context.UpcomingRides
                .Include(r => r.Rider)
                .Include(r => r.Instructor)
                .Include(r => r.Horse);

            return View(await rides.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.RiderId = new SelectList(_context.Riders, "Id", "LastName");
            ViewBag.InstructorId = new SelectList(_context.Instructors, "Id", "LastName");
            ViewBag.HorseId = new SelectList(_context.Horses, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UpcomingRide ride)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RiderId = new SelectList(_context.Riders, "Id", "LastName", ride.RiderId);
                ViewBag.InstructorId = new SelectList(_context.Instructors, "Id", "LastName", ride.InstructorId);
                ViewBag.HorseId = new SelectList(_context.Horses, "Id", "Name", ride.HorseId);
                return View(ride);
            }

            _context.Add(ride);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ride = await _context.UpcomingRides.FindAsync(id);
            if (ride == null) return NotFound();

            ViewBag.RiderId = new SelectList(_context.Riders, "Id", "LastName", ride.RiderId);
            ViewBag.InstructorId = new SelectList(_context.Instructors, "Id", "LastName", ride.InstructorId);
            ViewBag.HorseId = new SelectList(_context.Horses, "Id", "Name", ride.HorseId);
            return View(ride);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpcomingRide ride)
        {
            if (id != ride.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.RiderId = new SelectList(_context.Riders, "Id", "LastName", ride.RiderId);
                ViewBag.InstructorId = new SelectList(_context.Instructors, "Id", "LastName", ride.InstructorId);
                ViewBag.HorseId = new SelectList(_context.Horses, "Id", "Name", ride.HorseId);
                return View(ride);
            }

            _context.Update(ride);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ride = await _context.UpcomingRides
                .Include(r => r.Rider)
                .Include(r => r.Instructor)
                .Include(r => r.Horse)
                .FirstOrDefaultAsync(m => m.Id == id);

            return ride == null ? NotFound() : View(ride);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ride = await _context.UpcomingRides
                .Include(r => r.Rider)
                .Include(r => r.Instructor)
                .Include(r => r.Horse)
                .FirstOrDefaultAsync(m => m.Id == id);

            return ride == null ? NotFound() : View(ride);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ride = await _context.UpcomingRides.FindAsync(id);
            if (ride != null)
            {
                _context.UpcomingRides.Remove(ride);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
