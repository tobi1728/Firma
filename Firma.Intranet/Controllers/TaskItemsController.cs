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
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly List<string> _categories = new()
        {
            "Przegląd kopyt",
            "Konsultacja weterynaryjna",
            "Przegląd sprzętu",
            "Inne"
        };

        private readonly List<string> _statuses = new()
        {
            "Do zrobienia",
            "Zrobione"
        };

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TaskItems.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.TaskItems.FirstOrDefaultAsync(m => m.Id == id);
            return task == null ? NotFound() : View(task);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categories);
            ViewBag.Statuses = new SelectList(_statuses);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_categories, task.Category);
                ViewBag.Statuses = new SelectList(_statuses, task.Status);
                return View(task);
            }

            _context.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.TaskItems.FindAsync(id);
            if (task == null) return NotFound();

            ViewBag.Categories = new SelectList(_categories, task.Category);
            ViewBag.Statuses = new SelectList(_statuses, task.Status);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskItem task)
        {
            if (id != task.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_categories, task.Category);
                ViewBag.Statuses = new SelectList(_statuses, task.Status);
                return View(task);
            }

            _context.Update(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.TaskItems.FirstOrDefaultAsync(m => m.Id == id);
            return task == null ? NotFound() : View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
