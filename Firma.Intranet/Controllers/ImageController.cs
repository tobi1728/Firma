using Microsoft.AspNetCore.Mvc;
using Firma.Intranet.Data;

namespace Firma.Intranet.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("horse/{id}")]
        public IActionResult GetHorseImage(int id)
        {
            var horse = _context.Horses.FirstOrDefault(i => i.Id == id);

            if (horse == null || string.IsNullOrEmpty(horse.PhotoUrl))
                return NotFound();

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", horse.PhotoUrl.TrimStart('/'));

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            var contentType = "image/jpeg"; 
            return PhysicalFile(fullPath, contentType);
        }

        [HttpGet("instructor/{id}")]
        public IActionResult GetInstructorImage(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);

            if (instructor == null || string.IsNullOrEmpty(instructor.PhotoUrl))
                return NotFound();

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", instructor.PhotoUrl.TrimStart('/'));

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            var contentType = "image/jpeg"; 
            return PhysicalFile(fullPath, contentType);
        }

        [HttpGet("rider/{id}")]
        public IActionResult GetRiderImage(int id)
        {
            var rider = _context.Riders.FirstOrDefault(r => r.Id == id);

            if (rider == null || string.IsNullOrEmpty(rider.PhotoUrl))
                return NotFound();

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", rider.PhotoUrl.TrimStart('/'));

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            var contentType = "image/jpeg"; 
            return PhysicalFile(fullPath, contentType);
        }

    }
}
