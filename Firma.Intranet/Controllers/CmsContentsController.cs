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

        public async Task<IActionResult> Index()
        {
            return View(await _context.CmsContents.ToListAsync());
        }

        
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

        public IActionResult Create()
        {
            ViewBag.Pages = new SelectList(PageFields.Keys);
            ViewBag.Keys = new List<SelectListItem>(); 

            return View();
        }

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
            ["About"] = new()
            {
                ("Title", "Tytuł"),
                ("Subtitle", "Podtytuł"),
                ("Experience", "Liczba lat doświadczenia"),
                ("Horses", "Liczba koni"),
                ("Students", "Liczba uczniów"),
                ("Instructors", "Liczba instruktorów"),
                ("Card1Title", "Tytuł karty 1"),
                ("Card1Text", "Opis karty 1"),
                ("Card2Title", "Tytuł karty 2"),
                ("Card2Text", "Opis karty 2"),
                ("Card3Title", "Tytuł karty 3"),
                ("Card3Text", "Opis karty 3")
            }
,            ["All"] = new()
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
