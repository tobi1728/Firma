using Microsoft.AspNetCore.Mvc;
using Firma.PortalWWW.Data;
using Firma.PortalWWW.Models;
using Firma.PortalWWW.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Firma.PortalWWW.Controllers
{
    public class RiderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CmsApiClient _cmsClient;

        public RiderController(ApplicationDbContext context, CmsApiClient cmsClient)
        {
            _context = context;
            _cmsClient = cmsClient;
        }

        public async Task<IActionResult> Profile()
        {
            ViewBag.Title = await _cmsClient.GetContent("Profile", "Title") ?? "Mój profil";
            ViewBag.Subtitle = await _cmsClient.GetContent("Profile", "Subtitle") ?? "Zobacz dane przypisane do Twojego konta jeźdźca";

            int? riderId = HttpContext.Session.GetInt32("RiderId");
            if (riderId == null)
                return RedirectToAction("SelectRider", "Account");

            var rider = await _context.Riders.FindAsync(riderId);
            if (rider == null)
                return NotFound();

            return View(rider);
        }

        public async Task<IActionResult> Edit()
        {
            ViewBag.Title = await _cmsClient.GetContent("Edit", "Title") ?? "Edytuj profil";
            ViewBag.Subtitle = await _cmsClient.GetContent("Edit", "Subtitle") ?? "Zaktualizuj swoje dane jeździeckie";

            var riderId = HttpContext.Session.GetInt32("RiderId");
            if (riderId == null)
                return RedirectToAction("Select", "Account");

            var rider = await _context.Riders.FirstOrDefaultAsync(r => r.Id == riderId);
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
