using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Firma.Intranet.Data;
using Firma.Intranet.Models;

namespace Firma.Intranet.Controllers
{
    public class RidersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RidersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Riders.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rider = await _context.Riders.FirstOrDefaultAsync(r => r.Id == id);
            if (rider == null) return NotFound();

            return View(rider);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rider rider, IFormFile? photo)
        {
            if (!ModelState.IsValid)
                return View(rider);

            if (photo != null && photo.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
                var relativePath = "/content/images/riders/" + fileName;
                var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "images", "riders", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)!);
                using var stream = new FileStream(absolutePath, FileMode.Create);
                await photo.CopyToAsync(stream);
                rider.PhotoUrl = relativePath;
            }

            _context.Add(rider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var rider = await _context.Riders.FindAsync(id);
            return rider == null ? NotFound() : View(rider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rider updated, IFormFile? photo)
        {
            if (id != updated.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(updated);

            var rider = await _context.Riders.FindAsync(id);
            if (rider == null) return NotFound();

            rider.FirstName = updated.FirstName;
            rider.LastName = updated.LastName;
            rider.PhoneNumber = updated.PhoneNumber;
            rider.Age = updated.Age;
            rider.SkillLevel = updated.SkillLevel;
            rider.Weight = updated.Weight;

            if (photo != null && photo.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
                var relativePath = "/content/images/riders/" + fileName;
                var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "images", "riders", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)!);
                using var stream = new FileStream(absolutePath, FileMode.Create);
                await photo.CopyToAsync(stream);
                rider.PhotoUrl = relativePath;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var rider = await _context.Riders.FirstOrDefaultAsync(r => r.Id == id);
            return rider == null ? NotFound() : View(rider);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rider = await _context.Riders.FindAsync(id);
            if (rider != null)
            {
                _context.Riders.Remove(rider);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
