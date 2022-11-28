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
    public class PolozkyController : Controller
    {
        private readonly ModelContext _context;

        public PolozkyController(ModelContext context)
        {
            _context = context;
        }

        // GET: Polozky
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Polozky.Include(p => p.CisloProdejeNavigation).Include(p => p.IdZboziNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Polozky/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Polozky == null)
            {
                return NotFound();
            }

            var polozky = await _context.Polozky
                .Include(p => p.CisloProdejeNavigation)
                .Include(p => p.IdZboziNavigation)
                .FirstOrDefaultAsync(m => m.NazevZbozi == id);
            if (polozky == null)
            {
                return NotFound();
            }

            return View(polozky);
        }

        // GET: Polozky/Create
        public IActionResult Create()
        {
            ViewData["CisloProdeje"] = new SelectList(_context.Prodeje, "CisloProdeje", "CisloProdeje");
            ViewData["IdZbozi"] = new SelectList(_context.Zbozi, "IdZbozi", "NazevZbozi");
            return View();
        }

        // POST: Polozky/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZbozi,NazevZbozi,Mnozstvi,CisloProdeje")] Polozky polozky)
        {
            if (ModelState.IsValid)
            {
                _context.Add(polozky);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CisloProdeje"] = new SelectList(_context.Prodeje, "CisloProdeje", "CisloProdeje", polozky.CisloProdeje);
            ViewData["IdZbozi"] = new SelectList(_context.Zbozi, "IdZbozi", "NazevZbozi", polozky.IdZbozi);
            return View(polozky);
        }

        // GET: Polozky/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Polozky == null)
            {
                return NotFound();
            }

            var polozky = await _context.Polozky.FindAsync(id);
            if (polozky == null)
            {
                return NotFound();
            }
            ViewData["CisloProdeje"] = new SelectList(_context.Prodeje, "CisloProdeje", "CisloProdeje", polozky.CisloProdeje);
            ViewData["IdZbozi"] = new SelectList(_context.Zbozi, "IdZbozi", "NazevZbozi", polozky.IdZbozi);
            return View(polozky);
        }

        // POST: Polozky/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdZbozi,NazevZbozi,Mnozstvi,CisloProdeje")] Polozky polozky)
        {
            if (id != polozky.NazevZbozi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(polozky);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolozkyExists(polozky.NazevZbozi))
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
            ViewData["CisloProdeje"] = new SelectList(_context.Prodeje, "CisloProdeje", "CisloProdeje", polozky.CisloProdeje);
            ViewData["IdZbozi"] = new SelectList(_context.Zbozi, "IdZbozi", "NazevZbozi", polozky.IdZbozi);
            return View(polozky);
        }

        // GET: Polozky/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Polozky == null)
            {
                return NotFound();
            }

            var polozky = await _context.Polozky
                .Include(p => p.CisloProdejeNavigation)
                .Include(p => p.IdZboziNavigation)
                .FirstOrDefaultAsync(m => m.NazevZbozi == id);
            if (polozky == null)
            {
                return NotFound();
            }

            return View(polozky);
        }

        // POST: Polozky/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Polozky == null)
            {
                return Problem("Entity set 'ModelContext.Polozky'  is null.");
            }
            var polozky = await _context.Polozky.FindAsync(id);
            if (polozky != null)
            {
                _context.Polozky.Remove(polozky);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolozkyExists(string id)
        {
          return _context.Polozky.Any(e => e.NazevZbozi == id);
        }
    }
}
