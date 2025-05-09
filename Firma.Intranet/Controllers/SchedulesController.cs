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
    public class SchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var schedules = _context.Schedules.Include(s => s.Instructor);
            return View(await schedules.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var schedule = await _context.Schedules
                .Include(s => s.Instructor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (schedule == null) return NotFound();

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewBag.InstructorId = new SelectList(_context.Instructors, "Id", "LastName");
            return View();
        }

        // POST: Schedules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Schedule schedule)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.InstructorId = new SelectList(_context.Instructors, "Id", "LastName", schedule.InstructorId);
                return View(schedule);
            }

            _context.Add(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null) return NotFound();

            ViewBag.InstructorId = new SelectList(_context.Instructors, "Id", "LastName", schedule.InstructorId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Schedule schedule)
        {
            if (id != schedule.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.InstructorId = new SelectList(_context.Instructors, "Id", "LastName", schedule.InstructorId);
                return View(schedule);
            }

            try
            {
                _context.Update(schedule);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(schedule.Id)) return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var schedule = await _context.Schedules
                .Include(s => s.Instructor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (schedule == null) return NotFound();

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}
