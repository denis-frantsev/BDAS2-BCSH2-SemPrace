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
    public class PultyController : Controller
    {
        private readonly ModelContext _context;

        public PultyController(ModelContext context)
        {
            _context = context;
        }

        // GET: Pulty
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Pulty.Include(p => p.IdSupermarketNavigation).Include(p => p.NazevNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Pulty/Details/5
        public async Task<IActionResult> Details(int? cisloPultu, int? supermarketId)
        {
            if (cisloPultu == null || supermarketId == null || _context.Pulty == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var pulty = await _context.Pulty
                .Include(p => p.IdSupermarketNavigation)
                .Include(p => p.NazevNavigation)
                .FirstOrDefaultAsync(m => m.CisloPultu == cisloPultu && m.IdSupermarket == supermarketId);
            if (pulty == null)
            {
                return NotFound();
            }

            return View(pulty);
        }

        // GET: Pulty/Create
        public IActionResult Create()
        {
            if (!ModelContext.HasAdminRights())
                return NotFound();
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev");
            ViewData["Nazev"] = new SelectList(_context.NazvyPultu, "IdPult", "IdPult");
            return View();
        }

        // POST: Pulty/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CisloPultu,IdSupermarket,Nazev")] Pulty pulty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pulty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", pulty.IdSupermarket);
            ViewData["Nazev"] = new SelectList(_context.NazvyPultu, "IdPult", "IdPult", pulty.Nazev);
            return View(pulty);
        }

        // GET: Pulty/Edit/5
        public async Task<IActionResult> Edit(int? cisloPultu, int? supermarketId)
        {
            if (cisloPultu == null || supermarketId == null || _context.Pulty == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var pulty = await _context.Pulty.FindAsync(cisloPultu, supermarketId);
            if (pulty == null)
            {
                return NotFound();
            }
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", pulty.IdSupermarket);
            ViewData["Nazev"] = new SelectList(_context.NazvyPultu, "IdPult", "IdPult", pulty.Nazev);
            return View(pulty);
        }

        // POST: Pulty/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int cisloPultu, int supermarketId, [Bind("CisloPultu,IdSupermarket,Nazev")] Pulty pulty)
        {
            if (cisloPultu != pulty.CisloPultu && supermarketId != pulty.IdSupermarket)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pulty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PultyExists(pulty.CisloPultu, pulty.IdSupermarket))
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
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", pulty.IdSupermarket);
            ViewData["Nazev"] = new SelectList(_context.NazvyPultu, "IdPult", "IdPult", pulty.Nazev);
            return View(pulty);
        }

        // GET: Pulty/Delete/5
        //[HttpGet("{cisloPultu?}/{supermarketId?}"), ActionName("Delete")]

        public async Task<IActionResult> Delete(int? cisloPultu, int? supermarketId)
        {
            if (cisloPultu == null || _context.Pulty == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var pulty = await _context.Pulty
                .Include(p => p.IdSupermarketNavigation)
                .Include(p => p.NazevNavigation)
                .FirstOrDefaultAsync(m => m.CisloPultu == cisloPultu && m.IdSupermarket == supermarketId);
            if (pulty == null)
            {
                return NotFound();
            }

            return View(pulty);
        }

        // POST: Pulty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int cisloPultu, int supermarketId)
        {
            if (_context.Pulty == null)
            {
                return Problem("Entity set 'ModelContext.Pulty'  is null.");
            }
            var pulty = await _context.Pulty.FindAsync(cisloPultu, supermarketId);
            if (pulty != null)
            {
                _context.Pulty.Remove(pulty);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PultyExists(int idPult, int idSupermarket)
        {
            return _context.Pulty.Any(e => e.CisloPultu == idPult && e.IdSupermarket == idSupermarket);
        }
    }
}
