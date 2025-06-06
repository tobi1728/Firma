using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CmsService.Data;
using CmsService.Models;

namespace CmsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CmsEntryController : ControllerBase
    {
        private readonly CmsDbContext _context;

        public CmsEntryController(CmsDbContext context)
        {
            _context = context;
        }

        [HttpGet("{page}/{key}")]
        public async Task<IActionResult> GetByPageAndKey(string page, string key)
        {
            var entry = await _context.CmsEntries
                .FirstOrDefaultAsync(x => x.Page == page && x.Key == key);

            if (entry == null)
                return NotFound();

            return Ok(entry.Value); // tylko tekst jako string
        }

        [HttpGet("{page}")]
        public async Task<IActionResult> GetByPage(string page)
        {
            var items = await _context.CmsEntries
                .Where(x => x.Page == page)
                .ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CmsEntry entry)
        {
            _context.CmsEntries.Add(entry);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByPage), new { page = entry.Page }, entry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CmsEntry updated)
        {
            var entry = await _context.CmsEntries.FindAsync(id);
            if (entry == null) return NotFound();

            entry.Page = updated.Page;
            entry.Key = updated.Key;
            entry.Value = updated.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entry = await _context.CmsEntries.FindAsync(id);
            if (entry == null) return NotFound();

            _context.CmsEntries.Remove(entry);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
