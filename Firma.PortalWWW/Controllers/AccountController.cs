using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firma.PortalWWW.Data;
using Firma.PortalWWW.Models;

namespace Firma.PortalWWW.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/SelectRider
        public async Task<IActionResult> SelectRider()
        {
            var riders = await _context.Riders.ToListAsync();
            return View(riders);
        }

        // POST: /Account/LoginAsRider
        [HttpPost]
        public IActionResult LoginAsRider(int riderId)
        {
            HttpContext.Session.SetInt32("RiderId", riderId);
            return RedirectToAction("Profile", "Rider");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("RiderId");
            return RedirectToAction("Index", "Home");
        }
    }
}
