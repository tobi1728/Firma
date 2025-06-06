using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firma.PortalWWW.Data;
using Firma.PortalWWW.Services; 

namespace Firma.PortalWWW.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CmsApiClient _cmsClient;

        public AccountController(ApplicationDbContext context, CmsApiClient cmsClient)
        {
            _context = context;
            _cmsClient = cmsClient;
        }

        public async Task<IActionResult> SelectRider()
        {
            ViewBag.Title = await _cmsClient.GetContent("SelectRider", "Title") ?? "Zaloguj się jako jeździec";
            ViewBag.Subtitle = await _cmsClient.GetContent("SelectRider", "Subtitle") ?? "Wybierz jednego z dostępnych jeźdźców, aby przejść dalej";

            var riders = await _context.Riders.ToListAsync();
            return View(riders);
        }

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
