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
    public class ZnackyController : Controller
    {
        private readonly ModelContext _context;

        public ZnackyController(ModelContext context)
        {
            _context = context;
        }

        // GET: Znacky
        public async Task<IActionResult> Index()
        {
              return View(await _context.Znacky.ToListAsync());
        }

        // GET: Znacky/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Znacky == null)
            {
                return NotFound();
            }

            var znacky = await _context.Znacky
                .FirstOrDefaultAsync(m => m.IdZnacka == id);
            if (znacky == null)
            {
                return NotFound();
            }

            return View(znacky);
        }

        // GET: Znacky/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Znacky/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZnacka,Nazev")] Znacky znacky)
        {
            if (ModelState.IsValid)
            {
                _context.Add(znacky);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(znacky);
        }

        // GET: Znacky/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Znacky == null)
            {
                return NotFound();
            }

            var znacky = await _context.Znacky.FindAsync(id);
            if (znacky == null)
            {
                return NotFound();
            }
            return View(znacky);
        }

        // POST: Znacky/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdZnacka,Nazev")] Znacky znacky)
        {
            if (id != znacky.IdZnacka)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(znacky);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZnackyExists(znacky.IdZnacka))
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
            return View(znacky);
        }

        // GET: Znacky/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Znacky == null)
            {
                return NotFound();
            }

            var znacky = await _context.Znacky
                .FirstOrDefaultAsync(m => m.IdZnacka == id);
            if (znacky == null)
            {
                return NotFound();
            }

            return View(znacky);
        }

        // POST: Znacky/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Znacky == null)
            {
                return Problem("Entity set 'ModelContext.Znacky'  is null.");
            }
            var znacky = await _context.Znacky.FindAsync(id);
            if (znacky != null)
            {
                _context.Znacky.Remove(znacky);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZnackyExists(short id)
        {
          return _context.Znacky.Any(e => e.IdZnacka == id);
        }
    }
}
