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
    public class ZakazniciController : Controller
    {
        private readonly ModelContext _context;

        public ZakazniciController(ModelContext context)
        {
            _context = context;
        }

        // GET: Zakaznici
        public async Task<IActionResult> Index()
        {
              return View(await _context.Zakaznici.ToListAsync());
        }

        // GET: Zakaznici/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zakaznici == null)
            {
                return NotFound();
            }

            var zakaznici = await _context.Zakaznici
                .FirstOrDefaultAsync(m => m.IdZakaznik == id);
            if (zakaznici == null)
            {
                return NotFound();
            }

            return View(zakaznici);
        }

        // GET: Zakaznici/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zakaznici/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZakaznik,Jmeno,Prijmeni,TelefonniCislo,Email")] Zakaznici zakaznici)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zakaznici);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zakaznici);
        }

        // GET: Zakaznici/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zakaznici == null)
            {
                return NotFound();
            }

            var zakaznici = await _context.Zakaznici.FindAsync(id);
            if (zakaznici == null)
            {
                return NotFound();
            }
            return View(zakaznici);
        }

        // POST: Zakaznici/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdZakaznik,Jmeno,Prijmeni,TelefonniCislo,Email")] Zakaznici zakaznici)
        {
            if (id != zakaznici.IdZakaznik)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zakaznici);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZakazniciExists(zakaznici.IdZakaznik))
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
            return View(zakaznici);
        }

        // GET: Zakaznici/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zakaznici == null)
            {
                return NotFound();
            }

            var zakaznici = await _context.Zakaznici
                .FirstOrDefaultAsync(m => m.IdZakaznik == id);
            if (zakaznici == null)
            {
                return NotFound();
            }

            return View(zakaznici);
        }

        // POST: Zakaznici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zakaznici == null)
            {
                return Problem("Entity set 'ModelContext.Zakaznici'  is null.");
            }
            var zakaznici = await _context.Zakaznici.FindAsync(id);
            if (zakaznici != null)
            {
                _context.Zakaznici.Remove(zakaznici);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZakazniciExists(int id)
        {
          return _context.Zakaznici.Any(e => e.IdZakaznik == id);
        }
    }
}
