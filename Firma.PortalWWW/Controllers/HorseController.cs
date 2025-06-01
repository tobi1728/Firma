using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firma.PortalWWW.Data;
using Firma.PortalWWW.Models;
using System.Threading.Tasks;

namespace Firma.PortalWWW.Controllers
{
    public class HorseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HorseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Horses()
        {
            var horses = _context.Horses.ToList();
            return View(horses);
        }
    }
}
