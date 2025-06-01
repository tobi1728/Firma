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
        public async Task<IActionResult> Create(Horse horse, IFormFile? photo) 
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    var relativePath = "/content/images/horses/" + fileName;
                    var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "images", "horses", fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)!);

                    using (var stream = new FileStream(absolutePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    horse.PhotoUrl = relativePath;
                }

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
        public async Task<IActionResult> Edit(int id, Horse horse, IFormFile? photo)
        {
            if (id != horse.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingHorse = await _context.Horses.FindAsync(id);
                    if (existingHorse == null)
                        return NotFound();

                    existingHorse.Name = horse.Name;
                    existingHorse.Breed = horse.Breed;
                    existingHorse.Age = horse.Age;
                    existingHorse.Status = horse.Status;
                    existingHorse.LastCheckup = horse.LastCheckup;
                    existingHorse.WeightKg = horse.WeightKg;
                    existingHorse.HeightCm = horse.HeightCm;
                    existingHorse.MaxRiderLevel = horse.MaxRiderLevel;

                    if (photo != null && photo.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                        var relativePath = "/content/images/horses/" + fileName;
                        var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "images", "horses", fileName);

                        Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)!);

                        using (var stream = new FileStream(absolutePath, FileMode.Create))
                        {
                            await photo.CopyToAsync(stream);
                        }

                        existingHorse.PhotoUrl = relativePath;
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Horses", "Home");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorseExists(horse.Id))
                        return NotFound();
                    else
                        throw;
                }
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
