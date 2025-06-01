using Microsoft.AspNetCore.Mvc;
using Firma.PortalWWW.Data;
using Firma.PortalWWW.Models;

namespace Firma.PortalWWW.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Instructors()
        {
            var instructors = _context.Instructors.ToList();
            return View(instructors);
        }
    }
}
