using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BDAS2_SemPrace.Models;
using Oracle.ManagedDataAccess.Client;

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
            var test = await _context.Supermarkety.FromSqlRaw("select * from supermarkety_view").Include(s=>s.IdAdresaNavigation).ToListAsync();
            return View(test);
        }

        // GET: Supermarkety/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Supermarkety == null || !ModelContext.HasAdminRights())
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
            if (!ModelContext.HasAdminRights())
                return NotFound();
            ViewData["IdAdresa"] = new SelectList(_context.Adresy, "IdAdresa", "Mesto");
            return View();
        }

        // POST: Supermarkety/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nazev,IdAdresaNavigation")] Supermarkety supermarkety)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supermarkety);
                _context.Adresy.Add(supermarkety.IdAdresaNavigation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAdresa"] = new SelectList(_context.Adresy, "IdAdresa", "Mesto", supermarkety.IdAdresa);
            return View(supermarkety);
        }

        // GET: Supermarkety/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Supermarkety == null || !ModelContext.HasAdminRights())
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSupermarket,Nazev,IdAdresa")] Supermarkety supermarkety)
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Supermarkety == null || !ModelContext.HasAdminRights())
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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

        private bool SupermarketyExists(int id)
        {
          return _context.Supermarkety.Any(e => e.IdSupermarket == id);
        }
    }
}
