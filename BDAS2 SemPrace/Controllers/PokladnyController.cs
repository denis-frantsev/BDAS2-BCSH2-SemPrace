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

        // GET: Pokladnies
        public async Task<IActionResult> Index()
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();

            var modelContext = _context.Pokladny.Include(p => p.IdSupermarketNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Pokladnies/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Pokladny == null)
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

        // GET: Pokladnies/Create
        public IActionResult Create()
        {
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev");
            return View();
        }

        // POST: Pokladnies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Pokladnies/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Pokladny == null)
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

        // POST: Pokladnies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdSupermarket,CisloPokladny")] Pokladny pokladny)
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

        // GET: Pokladnies/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Pokladny == null)
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

        // POST: Pokladnies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
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

        private bool PokladnyExists(decimal id)
        {
          return _context.Pokladny.Any(e => e.IdSupermarket == id);
        }
    }
}
