using Microsoft.AspNetCore.Mvc;
using Firma.PortalWWW.Data;
using Firma.PortalWWW.Models;
using Firma.PortalWWW.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Firma.PortalWWW.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CmsApiClient _cmsClient;

        public ServiceController(ApplicationDbContext context, CmsApiClient cmsClient)
        {
            _context = context;
            _cmsClient = cmsClient;
        }

        public async Task<IActionResult> Services()
        {
            ViewBag.Title = await _cmsClient.GetContent("Services", "Title") ?? "Nasza Oferta";
            ViewBag.Subtitle = await _cmsClient.GetContent("Services", "Subtitle") ?? "Zobacz usługi dostępne w naszej stajni";

            var services = await _context.Services.ToListAsync();
            return View(services);
        }
    }
}
