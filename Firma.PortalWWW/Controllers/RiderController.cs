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
    }
}
