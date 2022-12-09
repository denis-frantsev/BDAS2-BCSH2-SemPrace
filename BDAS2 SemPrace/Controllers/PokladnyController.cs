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
    public class PokladnyController : Controller
    {
        private readonly ModelContext _context;

        public PokladnyController(ModelContext context)
        {
            _context = context;
        }

        // GET: Pokladny
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Pokladny.Include(p => p.IdSupermarketNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Pokladny/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pokladny == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var pokladny = await _context.Pokladny
                .Include(p => p.IdSupermarketNavigation)
                .FirstOrDefaultAsync(m => m.IdSupermarket == id);
            if (pokladny == null)
            {
                return NotFound();
            }

            return View(pokladny);
        }

        // GET: Pokladny/Create
        public IActionResult Create()
        {
            if (!ModelContext.HasAdminRights())
                return NotFound();
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev");
            return View();
        }

        // POST: Pokladny/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSupermarket,CisloPokladny")] Pokladny pokladny)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokladny);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", pokladny.IdSupermarket);
            return View(pokladny);
        }

        // GET: Pokladny/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pokladny == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var pokladny = await _context.Pokladny.FindAsync(id);
            if (pokladny == null)
            {
                return NotFound();
            }
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", pokladny.IdSupermarket);
            return View(pokladny);
        }

        // POST: Pokladny/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSupermarket,CisloPokladny")] Pokladny pokladny)
        {
            if (id != pokladny.IdSupermarket)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokladny);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokladnyExists(pokladny.IdSupermarket))
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
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", pokladny.IdSupermarket);
            return View(pokladny);
        }

        // GET: Pokladny/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pokladny == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var pokladny = await _context.Pokladny
                .Include(p => p.IdSupermarketNavigation)
                .FirstOrDefaultAsync(m => m.IdSupermarket == id);
            if (pokladny == null)
            {
                return NotFound();
            }

            return View(pokladny);
        }

        // POST: Pokladny/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pokladny == null)
            {
                return Problem("Entity set 'ModelContext.Pokladny'  is null.");
            }
            var pokladny = await _context.Pokladny.FindAsync(id);
            if (pokladny != null)
            {
                _context.Pokladny.Remove(pokladny);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokladnyExists(int id)
        {
          return _context.Pokladny.Any(e => e.IdSupermarket == id);
        }
    }
}
