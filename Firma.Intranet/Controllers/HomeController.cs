using Firma.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Firma.Intranet.Data;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Horses()
        {
            var horses = await _context.Horses.ToListAsync();

            ViewBag.ActiveCount = horses.Count(h => h.Status == "Aktywny");
            ViewBag.RestingCount = horses.Count(h => h.Status == "W odpoczynku");
            ViewBag.UnavailableCount = horses.Count(h => h.Status == "Niedostêpny");

            var today = DateTime.Today;
            var upcomingChecks = horses
                .Where(h => h.LastCheckup.HasValue)
                .Select(h => new
                {
                    Horse = h,
                    NextCheck = h.LastCheckup.Value.AddDays(180)
                })
                .Where(x => x.NextCheck >= today && x.NextCheck <= today.AddDays(30))
                .OrderBy(x => x.NextCheck)
                .ToList();

            ViewBag.UpcomingChecks = upcomingChecks;

            return View(horses);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Schedule()
        {
            return View();
        }

        public IActionResult Reports()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
