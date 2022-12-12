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
    public class SkladyController : Controller
    {
        private readonly ModelContext _context;

        public SkladyController(ModelContext context)
        {
            _context = context;
        }

        // GET: Sklady
        public async Task<IActionResult> Index()
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();

            var modelContext = _context.Sklady.Include(s => s.IdAdresaNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Sklady/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Sklady == null)
            {
                return NotFound();
            }

            var sklady = await _context.Sklady
                .Include(s => s.IdAdresaNavigation)
                .FirstOrDefaultAsync(m => m.IdSklad == id);
            if (sklady == null)
            {
                return NotFound();
            }

            return View(sklady);
        }

        // GET: Sklady/Create
        public IActionResult Create()
        {
            ViewData["IdAdresa"] = new SelectList(_context.Adresy, "IdAdresa", "Mesto");
            return View();
        }

        // POST: Sklady/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSklad,Nazev,IdAdresa")] Sklady sklady)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sklady);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAdresa"] = new SelectList(_context.Adresy, "IdAdresa", "Mesto", sklady.IdAdresa);
            return View(sklady);
        }

        // GET: Sklady/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Sklady == null)
            {
                return NotFound();
            }

            var sklady = await _context.Sklady.FindAsync(id);
            if (sklady == null)
            {
                return NotFound();
            }
            ViewData["IdAdresa"] = new SelectList(_context.Adresy, "IdAdresa", "Mesto", sklady.IdAdresa);
            return View(sklady);
        }

        // POST: Sklady/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdSklad,Nazev,IdAdresa")] Sklady sklady)
        {
            if (id != sklady.IdSklad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sklady);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkladyExists(sklady.IdSklad))
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
            ViewData["IdAdresa"] = new SelectList(_context.Adresy, "IdAdresa", "Mesto", sklady.IdAdresa);
            return View(sklady);
        }

        // GET: Sklady/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Sklady == null)
            {
                return NotFound();
            }

            var sklady = await _context.Sklady
                .Include(s => s.IdAdresaNavigation)
                .FirstOrDefaultAsync(m => m.IdSklad == id);
            if (sklady == null)
            {
                return NotFound();
            }

            return View(sklady);
        }

        // POST: Sklady/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Sklady == null)
            {
                return Problem("Entity set 'ModelContext.Sklady'  is null.");
            }
            var sklady = await _context.Sklady.FindAsync(id);
            if (sklady != null)
            {
                _context.Sklady.Remove(sklady);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkladyExists(decimal id)
        {
          return _context.Sklady.Any(e => e.IdSklad == id);
        }
    }
}
