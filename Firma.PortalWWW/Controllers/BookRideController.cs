using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firma.PortalWWW.Data;
using Firma.PortalWWW.Models;

namespace Firma.PortalWWW.Controllers
{
    public class BookRideController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookRideController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Book(string? service)
        {
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
