using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firma.Intranet.Data;
using Firma.Intranet.Models;

namespace Firma.Intranet.Controllers
{
    public class CmsContentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CmsContentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CmsContents
        public async Task<IActionResult> Index()
        {
            return View(await _context.CmsContents.ToListAsync());
        }

        // GET: CmsContents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cmsContent = await _context.CmsContents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cmsContent == null)
            {
                return NotFound();
            }

            return View(cmsContent);
        }

        // GET: CmsContents/Create
        public IActionResult Create()
        {
            ViewBag.Pages = new SelectList(PageFields.Keys);
            ViewBag.Keys = new List<SelectListItem>(); // start empty

            return View();
        }

        // POST: CmsContents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Page,Key,Value")] CmsContent cmsContent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cmsContent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cmsContent);
        }

        // GET: CmsContents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cmsContent = await _context.CmsContents.FindAsync(id);
            if (cmsContent == null)
            {
                return NotFound();
            }
            return View(cmsContent);
        }

        // POST: CmsContents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Page,Key,Value")] CmsContent cmsContent)
        {
            if (id != cmsContent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cmsContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CmsContentExists(cmsContent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cmsContent);
        }

        // GET: CmsContents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cmsContent = await _context.CmsContents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cmsContent == null)
            {
                return NotFound();
            }

            return View(cmsContent);
        }

        // POST: CmsContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cmsContent = await _context.CmsContents.FindAsync(id);
            if (cmsContent != null)
            {
                _context.CmsContents.Remove(cmsContent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CmsContentExists(int id)
        {
            return _context.CmsContents.Any(e => e.Id == id);
        }

        private static readonly Dictionary<string, List<(string Key, string Label)>> PageFields = new()
        {
            ["Index"] = new() { ("Title", "Tytuł"), ("Subtitle", "Podtytuł") },
            ["About"] = new() { ("Title", "Tytuł"), ("Subtitle", "Podtytuł"), ("Card1", "Karta 1"), ("Card2", "Karta 2"), ("Card3", "Karta 3") },
            ["All"] = new()
    {
        ("header-about", "Header - O nas"),
        ("header-horses", "Header - Konie"),
        ("header-instructors", "Header - Instruktorzy"),
        ("header-services", "Header - Oferta"),
        ("header-book", "Header - Zarezerwuj jazdę"),
        ("header-login", "Header - Zaloguj się"),
        ("header-logout", "Header - Wyloguj"),
        ("header-profile", "Header - Profil"),
        ("footer-description", "Footer - Opis"),
        ("footer-address", "Footer - Adres"),
        ("footer-phone", "Footer - Telefon"),
        ("footer-email", "Footer - E-mail"),
        ("footer-working-days", "Footer - Dni robocze"),
        ("footer-weekends", "Footer - Weekend")
    }
        };

    }
}
