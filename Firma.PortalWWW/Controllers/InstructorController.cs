using Microsoft.AspNetCore.Mvc;
using Firma.PortalWWW.Data;
using Firma.PortalWWW.Models;
using Firma.PortalWWW.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Firma.PortalWWW.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CmsApiClient _cmsClient;

        public InstructorController(ApplicationDbContext context, CmsApiClient cmsClient)
        {
            _context = context;
            _cmsClient = cmsClient;
        }

        public async Task<IActionResult> Instructors()
        {
            ViewBag.Title = await _cmsClient.GetContent("Instructors", "Title") ?? "Nasi Instruktorzy";
            ViewBag.Subtitle = await _cmsClient.GetContent("Instructors", "Subtitle") ?? "Poznaj osoby, które poprowadzą Cię przez świat jeździectwa";

            var instructors = await _context.Instructors.ToListAsync();
            return View(instructors);
        }
    }
}
