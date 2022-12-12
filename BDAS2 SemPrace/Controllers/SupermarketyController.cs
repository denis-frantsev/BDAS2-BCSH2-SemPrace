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
    public class SupermarketyController : Controller
    {
        private readonly ModelContext _context;

        public SupermarketyController(ModelContext context)
        {
            _context = context;
        }

        // GET: Supermarkety
        public async Task<IActionResult> Index()
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();

            var modelContext = _context.Supermarkety.Include(s => s.IdAdresaNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Supermarkety/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Supermarkety == null)
            {
                return NotFound();
            }

            var supermarkety = await _context.Supermarkety
                .Include(s => s.IdAdresaNavigation)
                .FirstOrDefaultAsync(m => m.IdSupermarket == id);
            if (supermarkety == null)
            {
                return NotFound();
            }

            return View(supermarkety);
        }

        // GET: Supermarkety/Create
        public IActionResult Create()
        {
            ViewData["IdAdresa"] = new SelectList(_context.Adresy, "IdAdresa", "Mesto");
            return View();
        }

        // POST: Supermarkety/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSupermarket,Nazev,IdAdresa")] Supermarkety supermarkety)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supermarkety);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAdresa"] = new SelectList(_context.Adresy, "IdAdresa", "Mesto", supermarkety.IdAdresa);
            return View(supermarkety);
        }

        // GET: Supermarkety/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Supermarkety == null)
            {
                return NotFound();
            }

            var supermarkety = await _context.Supermarkety.FindAsync(id);
            if (supermarkety == null)
            {
                return NotFound();
            }
            ViewData["IdAdresa"] = new SelectList(_context.Adresy, "IdAdresa", "Mesto", supermarkety.IdAdresa);
            return View(supermarkety);
        }

        // POST: Supermarkety/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdSupermarket,Nazev,IdAdresa")] Supermarkety supermarkety)
        {
            if (id != supermarkety.IdSupermarket)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supermarkety);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupermarketyExists(supermarkety.IdSupermarket))
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
            ViewData["IdAdresa"] = new SelectList(_context.Adresy, "IdAdresa", "Mesto", supermarkety.IdAdresa);
            return View(supermarkety);
        }

        // GET: Supermarkety/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Supermarkety == null)
            {
                return NotFound();
            }

            var supermarkety = await _context.Supermarkety
                .Include(s => s.IdAdresaNavigation)
                .FirstOrDefaultAsync(m => m.IdSupermarket == id);
            if (supermarkety == null)
            {
                return NotFound();
            }

            return View(supermarkety);
        }

        // POST: Supermarkety/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Supermarkety == null)
            {
                return Problem("Entity set 'ModelContext.Supermarkety'  is null.");
            }
            var supermarkety = await _context.Supermarkety.FindAsync(id);
            if (supermarkety != null)
            {
                _context.Supermarkety.Remove(supermarkety);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupermarketyExists(decimal id)
        {
          return _context.Supermarkety.Any(e => e.IdSupermarket == id);
        }
    }
}
