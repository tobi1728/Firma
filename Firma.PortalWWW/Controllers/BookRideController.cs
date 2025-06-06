using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firma.PortalWWW.Data;
using Firma.PortalWWW.Models;
using Firma.PortalWWW.Services;
using System.Threading.Tasks;

namespace Firma.PortalWWW.Controllers
{
    public class BookRideController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CmsApiClient _cmsClient;

        public BookRideController(ApplicationDbContext context, CmsApiClient cmsClient)
        {
            _context = context;
            _cmsClient = cmsClient;
        }

        [HttpGet]
        public async Task<IActionResult> Book(string? service)
        {
            ViewBag.Title = await _cmsClient.GetContent("Book", "Title") ?? "Rezerwuj jazdę";

            var riderId = HttpContext.Session.GetInt32("RiderId");
            if (riderId == null)
                return RedirectToAction("SelectRider", "Account");

            var rider = await _context.Riders.FindAsync(riderId);
            var services = await _context.Services.ToListAsync();
            var horses = await _context.Horses.ToListAsync();

            var instructors = string.IsNullOrEmpty(service)
                ? new List<Instructor>()
                : await _context.Instructors
                    .Where(i => i.RidingType == service)
                    .ToListAsync();

            ViewBag.Rider = rider;
            ViewBag.PreselectedService = service;
            ViewBag.Horses = horses;
            ViewBag.Instructors = instructors;

            return View(services);
        }

        [HttpPost]
        public async Task<IActionResult> Book(string service, int horseId, int instructorId, DateTime date)
        {
            var riderId = HttpContext.Session.GetInt32("RiderId");
            if (riderId == null)
                return RedirectToAction("SelectRider", "Account");

            var ride = new UpcomingRide
            {
                RiderId = riderId.Value,
                HorseId = horseId,
                InstructorId = instructorId,
                RideDate = date,
                Notes = service,
                IsConfirmed = false
            };

            _context.UpcomingRides.Add(ride);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> MyRides()
        {
            ViewBag.Title = await _cmsClient.GetContent("MyRides", "Title") ?? "Moje jazdy";
            ViewBag.Subtitle = await _cmsClient.GetContent("MyRides", "Subtitle") ?? "Zobacz historię i nadchodzące jazdy";

            var riderId = HttpContext.Session.GetInt32("RiderId");
            if (riderId == null)
                return RedirectToAction("SelectRider", "Account");

            var rides = await _context.UpcomingRides
                .Include(r => r.Horse)
                .Include(r => r.Instructor)
                .Where(r => r.RiderId == riderId)
                .ToListAsync();

            return View(rides);
        }
    }
}
