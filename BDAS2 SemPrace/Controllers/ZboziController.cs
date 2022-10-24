using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BDAS2_SemPrace.Models;

namespace BDAS2_SemPrace.Controllers
{
    public class ZboziController : Controller
    {
        private readonly ModelContext _context;

        public ZboziController(ModelContext context)
        {
            _context = context;
        }

        // GET: Zbozi
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Zbozis.Include(z => z.IdKategorieNavigation).Include(z => z.IdZnackaNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Zbozi/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Zbozis == null)
            {
                return NotFound();
            }

            var zbozi = await _context.Zbozis
                .Include(z => z.IdKategorieNavigation)
                .Include(z => z.IdZnackaNavigation)
                .FirstOrDefaultAsync(m => m.IdZbozi == id);
            if (zbozi == null)
            {
                return NotFound();
            }

            return View(zbozi);
        }

        // GET: Zbozi/Create
        public IActionResult Create()
        {
            ViewData["IdKategorie"] = new SelectList(_context.Kategorie, "IdKategorie", "Nazev");
            ViewData["IdZnacka"] = new SelectList(_context.Znackies, "IdZnacka", "Nazev");
            return View();
        }

        // POST: Zbozi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZbozi,KodZbozi,NazevZbozi,IdKategorie,IdZnacka,Popis,Cena")] Zbozi zbozi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zbozi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKategorie"] = new SelectList(_context.Kategorie, "IdKategorie", "Nazev", zbozi.IdKategorie);
            ViewData["IdZnacka"] = new SelectList(_context.Znackies, "IdZnacka", "Nazev", zbozi.IdZnacka);
            return View(zbozi);
        }

        // GET: Zbozi/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Zbozis == null)
            {
                return NotFound();
            }

            var zbozi = await _context.Zbozis.FindAsync(id);
            if (zbozi == null)
            {
                return NotFound();
            }
            ViewData["IdKategorie"] = new SelectList(_context.Kategorie, "IdKategorie", "Nazev", zbozi.IdKategorie);
            ViewData["IdZnacka"] = new SelectList(_context.Znackies, "IdZnacka", "Nazev", zbozi.IdZnacka);
            return View(zbozi);
        }

        // POST: Zbozi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdZbozi,KodZbozi,NazevZbozi,IdKategorie,IdZnacka,Popis,Cena")] Zbozi zbozi)
        {
            if (id != zbozi.IdZbozi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zbozi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZboziExists(zbozi.IdZbozi))
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
            ViewData["IdKategorie"] = new SelectList(_context.Kategorie, "IdKategorie", "Nazev", zbozi.IdKategorie);
            ViewData["IdZnacka"] = new SelectList(_context.Znackies, "IdZnacka", "Nazev", zbozi.IdZnacka);
            return View(zbozi);
        }

        // GET: Zbozi/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Zbozis == null)
            {
                return NotFound();
            }

            var zbozi = await _context.Zbozis
                .Include(z => z.IdKategorieNavigation)
                .Include(z => z.IdZnackaNavigation)
                .FirstOrDefaultAsync(m => m.IdZbozi == id);
            if (zbozi == null)
            {
                return NotFound();
            }

            return View(zbozi);
        }

        // POST: Zbozi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Zbozis == null)
            {
                return Problem("Entity set 'ModelContext.Zbozis'  is null.");
            }
            var zbozi = await _context.Zbozis.FindAsync(id);
            if (zbozi != null)
            {
                _context.Zbozis.Remove(zbozi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZboziExists(decimal id)
        {
          return _context.Zbozis.Any(e => e.IdZbozi == id);
        }
    }
}
