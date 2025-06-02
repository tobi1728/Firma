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

        public async Task<IActionResult> Book(string? service)
        {
            var riderId = HttpContext.Session.GetInt32("RiderId");
            if (riderId == null)
                return RedirectToAction("SelectRider", "Account");

            ViewBag.PreselectedService = service;

            var services = await _context.Services.ToListAsync();
            return View(services);
        }

        // TODO: Dodaj akcję POST do rezerwowania, gdy dodamy model RideReservation
    }
}
