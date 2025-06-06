using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firma.PortalWWW.Data;
using Firma.PortalWWW.Models;
using Firma.PortalWWW.Services;
using System.Threading.Tasks;

namespace Firma.PortalWWW.Controllers
{
    public class HorseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CmsApiClient _cmsClient;

        public HorseController(ApplicationDbContext context, CmsApiClient cmsClient)
        {
            _context = context;
            _cmsClient = cmsClient;
        }

        public async Task<IActionResult> Horses()
        {
            ViewBag.Title = await _cmsClient.GetContent("Horses", "Title") ?? "Nasze Konie";
            ViewBag.Subtitle = await _cmsClient.GetContent("Horses", "Subtitle") ?? "Poznaj naszych czworonożnych przyjaciół, którzy pomogą Ci w nauce jeździectwa";

            var horses = await _context.Horses.ToListAsync();
            return View(horses);
        }
    }
}
