using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firma.Intranet.Data;
using Firma.Intranet.Models;

namespace Firma.Intranet.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        private readonly List<string> _ridingTypes = new()
        {
            "Ujeżdżeniowa",
            "Terenowa",
            "Rekreacyjna",
            "Skoki",
            "Sportowa",
            "Western"
        };

        public InstructorsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Instructors.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.Id == id);
            if (instructor == null) return NotFound();

            return View(instructor);
        }

        public IActionResult Create()
        {
            ViewBag.RidingTypes = new SelectList(_ridingTypes);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor, IFormFile Photo)
        {
            if (!ModelState.IsValid)
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Count > 0)
                    {
                        Console.WriteLine($"Model error on {key}: {state.Errors[0].ErrorMessage}");
                    }
                }
            {
                ViewBag.RidingTypes = new SelectList(_ridingTypes);
                return View(instructor);
            }

            if (Photo != null && Photo.Length > 0)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Photo.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Photo.CopyToAsync(stream);
                }

                instructor.PhotoUrl = "/uploads/" + fileName;
            }

            _context.Add(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null) return NotFound();

            ViewBag.RidingTypes = new SelectList(_ridingTypes, instructor.RidingType);
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instructor instructor, IFormFile? Photo)
        {
            if (id != instructor.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.RidingTypes = new SelectList(_ridingTypes, instructor.RidingType);
                return View(instructor);
            }

            var instructorInDb = await _context.Instructors.FindAsync(id);
            if (instructorInDb == null) return NotFound();

            instructorInDb.FirstName = instructor.FirstName;
            instructorInDb.LastName = instructor.LastName;
            instructorInDb.YearsOfExperience = instructor.YearsOfExperience;
            instructorInDb.RidingType = instructor.RidingType;
            instructorInDb.Salary = instructor.Salary;

            if (Photo != null && Photo.Length > 0)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Photo.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Photo.CopyToAsync(stream);
                }

                instructorInDb.PhotoUrl = "/uploads/" + fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.Id == id);
            if (instructor == null) return NotFound();

            return View(instructor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
