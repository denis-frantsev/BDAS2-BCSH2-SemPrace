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

        // GET: Prodejes
        public async Task<IActionResult> Index()
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();

            var modelContext = _context.Prodeje.Include(p => p.IdPlatbaNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Prodejes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prodeje == null)
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

        // GET: Prodejes/Create
        public IActionResult Create()
        {
            ViewData["IdPlatba"] = new SelectList(_context.Platby, "IdPlatba", "Typ");
            return View();
        }

        // POST: Prodejes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Prodejes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prodeje == null)
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

        // POST: Prodejes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Prodejes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prodeje == null)
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

        // POST: Prodejes/Delete/5
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
