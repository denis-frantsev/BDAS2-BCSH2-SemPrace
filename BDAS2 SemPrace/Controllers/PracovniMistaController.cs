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
    public class PracovniMistaController : Controller
    {
        private readonly ModelContext _context;

        public PracovniMistaController(ModelContext context)
        {
            _context = context;
        }

        // GET: PracovniMista
        public async Task<IActionResult> Index()
        {
              return View(await _context.PracovniMista.ToListAsync());
        }

        // GET: PracovniMista/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.PracovniMista == null)
            {
                return NotFound();
            }

            var pracovniMista = await _context.PracovniMista
                .FirstOrDefaultAsync(m => m.IdMisto == id);
            if (pracovniMista == null)
            {
                return NotFound();
            }

            return View(pracovniMista);
        }

        // GET: PracovniMista/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PracovniMista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMisto,Nazev,Popis,MinPlat")] PracovniMista pracovniMista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pracovniMista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pracovniMista);
        }

        // GET: PracovniMista/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.PracovniMista == null)
            {
                return NotFound();
            }

            var pracovniMista = await _context.PracovniMista.FindAsync(id);
            if (pracovniMista == null)
            {
                return NotFound();
            }
            return View(pracovniMista);
        }

        // POST: PracovniMista/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdMisto,Nazev,Popis,MinPlat")] PracovniMista pracovniMista)
        {
            if (id != pracovniMista.IdMisto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pracovniMista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PracovniMistaExists(pracovniMista.IdMisto))
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
            return View(pracovniMista);
        }

        // GET: PracovniMista/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.PracovniMista == null)
            {
                return NotFound();
            }

            var pracovniMista = await _context.PracovniMista
                .FirstOrDefaultAsync(m => m.IdMisto == id);
            if (pracovniMista == null)
            {
                return NotFound();
            }

            return View(pracovniMista);
        }

        // POST: PracovniMista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.PracovniMista == null)
            {
                return Problem("Entity set 'ModelContext.PracovniMista'  is null.");
            }
            var pracovniMista = await _context.PracovniMista.FindAsync(id);
            if (pracovniMista != null)
            {
                _context.PracovniMista.Remove(pracovniMista);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PracovniMistaExists(decimal id)
        {
          return _context.PracovniMista.Any(e => e.IdMisto == id);
        }
    }
}
