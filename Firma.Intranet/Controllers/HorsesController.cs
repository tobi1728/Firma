using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firma.Intranet.Data;
using Firma.Intranet.Models;
using System.Collections.Generic;

namespace Firma.Intranet.Controllers
{
    public class HorsesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HorsesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var horses = await _context.Horses.ToListAsync();

            ViewBag.ActiveCount = horses.Count(h => h.Status == "Aktywny");
            ViewBag.RestingCount = horses.Count(h => h.Status == "W odpoczynku");
            ViewBag.UnavailableCount = horses.Count(h => h.Status == "Niedostępny");

            return View(horses);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var horse = await _context.Horses.FirstOrDefaultAsync(m => m.Id == id);
            if (horse == null) return NotFound();

            return View(horse);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Horse horse, IFormFile? photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var horse = await _context.Horses.FindAsync(id);
            if (horse == null) return NotFound();

            return View(horse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Horse horse, IFormFile? photo)
        {
            if (id != horse.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingHorse = await _context.Horses.FindAsync(id);
                if (existingHorse == null) return NotFound();

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
                    var fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
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

            return View(horse);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var horse = await _context.Horses.FirstOrDefaultAsync(m => m.Id == id);
            if (horse == null) return NotFound();

            return View(horse);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMultipleConfirmed(int[] ids)
        {
            var horsesToDelete = await _context.Horses.Where(h => ids.Contains(h.Id)).ToListAsync();
            if (horsesToDelete.Any())
            {
                _context.Horses.RemoveRange(horsesToDelete);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Horses", "Home");
        }

        private bool HorseExists(int id)
        {
            return _context.Horses.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult ImportCsv()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportCsv(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                ModelState.AddModelError("", "Nie wybrano pliku CSV.");
                return View();
            }

            using var reader = new StreamReader(csvFile.OpenReadStream());
            string? line;
            int lineNumber = 0;
            var newHorses = new List<Horse>();

            while ((line = await reader.ReadLineAsync()) != null)
            {
                lineNumber++;
                if (lineNumber == 1) continue; // pomiń nagłówek

                var parts = line.Split(',');

                if (parts.Length < 9) continue;

                try
                {
                    var horse = new Horse
                    {
                        Name = parts[0],
                        Breed = parts[1],
                        Age = int.TryParse(parts[2], out var age) ? age : 0,
                        Status = parts[3],
                        LastCheckup = DateTime.TryParse(parts[4], out var checkup) ? checkup : null,
                        WeightKg = int.TryParse(parts[5], out var weight) ? weight : null,
                        HeightCm = int.TryParse(parts[6], out var height) ? height : null,
                        MaxRiderLevel = parts[7],
                        PhotoUrl = parts[8]
                    };

                    newHorses.Add(horse);
                }
                catch
                {
                    continue;
                }
            }

            if (newHorses.Any())
            {
                _context.Horses.AddRange(newHorses);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Horses", "Home");
        }

        public async Task<IActionResult> PrintCard(int id)
        {
            var horse = await _context.Horses.FindAsync(id);
            if (horse == null) return NotFound();

            return View("PrintCard", horse);
    }

}