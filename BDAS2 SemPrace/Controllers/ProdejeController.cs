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
    public class ProdejeController : Controller
    {
        private readonly ModelContext _context;

        public ProdejeController(ModelContext context)
        {
            _context = context;
        }

        // GET: Prodeje
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Prodeje.Include(p => p.IdPlatbaNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Prodeje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prodeje == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var prodeje = await _context.Prodeje
                .Include(p => p.IdPlatbaNavigation)
                .Include(p => p.Polozky)
                .FirstOrDefaultAsync(m => m.CisloProdeje == id);
            if (prodeje == null)
            {
                return NotFound();
            }

            return View(prodeje);
        }

        // GET: Prodeje/Create
        public IActionResult Create()
        {
            if (!ModelContext.HasAdminRights())
                return NotFound();
            ViewData["IdPlatba"] = new SelectList(_context.Platby, "IdPlatba", "Typ");
            return View();
        }

        // POST: ProdejesCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CisloProdeje,Suma,Datum,IdPlatba")] Prodeje prodeje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prodeje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPlatba"] = new SelectList(_context.Platby, "IdPlatba", "Typ", prodeje.IdPlatba);
            return View(prodeje);
        }

        // GET: Prodeje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prodeje == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var prodeje = await _context.Prodeje.FindAsync(id);
            if (prodeje == null)
            {
                return NotFound();
            }
            ViewData["IdPlatba"] = new SelectList(_context.Platby, "IdPlatba", "Typ", prodeje.IdPlatba);
            return View(prodeje);
        }

        // POST: Prodeje/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CisloProdeje,Suma,Datum,IdPlatba")] Prodeje prodeje)
        {
            if (id != prodeje.CisloProdeje)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prodeje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdejeExists(prodeje.CisloProdeje))
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
            ViewData["IdPlatba"] = new SelectList(_context.Platby, "IdPlatba", "Typ", prodeje.IdPlatba);
            return View(prodeje);
        }

        // GET: Prodeje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prodeje == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var prodeje = await _context.Prodeje
                .Include(p => p.IdPlatbaNavigation)
                .FirstOrDefaultAsync(m => m.CisloProdeje == id);
            if (prodeje == null)
            {
                return NotFound();
            }

            return View(prodeje);
        }

        // POST: Prodeje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prodeje == null)
            {
                return Problem("Entity set 'ModelContext.Prodeje'  is null.");
            }
            var prodeje = await _context.Prodeje.FindAsync(id);
            if (prodeje != null)
            {
                _context.Prodeje.Remove(prodeje);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdejeExists(int id)
        {
          return _context.Prodeje.Any(e => e.CisloProdeje == id);
        }
    }
}
