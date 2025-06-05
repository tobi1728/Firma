using Microsoft.AspNetCore.Mvc;
using Firma.PortalWWW.Data;
using Firma.PortalWWW.Models;

namespace Firma.PortalWWW.Controllers
{
    public class RiderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RiderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Profile()
        {
            int? riderId = HttpContext.Session.GetInt32("RiderId");
            if (riderId == null)
                return RedirectToAction("SelectRider", "Account");

            var rider = await _context.Riders.FindAsync(riderId);
            if (rider == null)
                return NotFound();

            return View(rider);
        }

        public IActionResult Edit()
        {
            var riderId = HttpContext.Session.GetInt32("RiderId");
            if (riderId == null)
                return RedirectToAction("Select", "Account");

            var rider = _context.Riders.FirstOrDefault(r => r.Id == riderId);
            if (rider == null)
                return NotFound();

            return View(rider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Rider model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existing = _context.Riders.FirstOrDefault(r => r.Id == model.Id);
            if (existing == null)
                return NotFound();

            existing.FirstName = model.FirstName;
            existing.LastName = model.LastName;
            existing.PhoneNumber = model.PhoneNumber;
            existing.Age = model.Age;
            existing.SkillLevel = model.SkillLevel;
            existing.Weight = model.Weight;

            _context.SaveChanges();

            return RedirectToAction("Profile");
        }

    }
}
