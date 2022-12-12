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
    public class ZamestnanciController : Controller
    {
        private readonly ModelContext _context;

        public ZamestnanciController(ModelContext context)
        {
            _context = context;
        }

        // GET: Zamestnanci
        public async Task<IActionResult> Index()
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();

            var modelContext = _context.Zamestnanci.Include(z => z.IdManazerNavigation).Include(z => z.IdMistoNavigation).Include(z => z.IdSkladNavigation).Include(z => z.IdSupermarketNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Zamestnanci/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Zamestnanci == null)
            {
                return NotFound();
            }

            var zamestnanci = await _context.Zamestnanci
                .Include(z => z.IdManazerNavigation)
                .Include(z => z.IdMistoNavigation)
                .Include(z => z.IdSkladNavigation)
                .Include(z => z.IdSupermarketNavigation)
                .FirstOrDefaultAsync(m => m.IdZamestnanec == id);
            if (zamestnanci == null)
            {
                return NotFound();
            }

            return View(zamestnanci);
        }

        // GET: Zamestnanci/Create
        public IActionResult Create()
        {
            ViewData["IdManazer"] = new SelectList(_context.Zamestnanci, "IdZamestnanec", "Email");
            ViewData["IdMisto"] = new SelectList(_context.PracovniMista, "IdMisto", "Nazev");
            ViewData["IdSklad"] = new SelectList(_context.Sklady, "IdSklad", "Nazev");
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev");
            return View();
        }

        // POST: Zamestnanci/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZamestnanec,Jmeno,Prijmeni,TelefonniCislo,Email,Mzda,IdSupermarket,IdManazer,IdMisto,IdSklad")] Zamestnanci zamestnanci)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zamestnanci);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdManazer"] = new SelectList(_context.Zamestnanci, "IdZamestnanec", "Email", zamestnanci.IdManazer);
            ViewData["IdMisto"] = new SelectList(_context.PracovniMista, "IdMisto", "Nazev", zamestnanci.IdMisto);
            ViewData["IdSklad"] = new SelectList(_context.Sklady, "IdSklad", "Nazev", zamestnanci.IdSklad);
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", zamestnanci.IdSupermarket);
            return View(zamestnanci);
        }

        // GET: Zamestnanci/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Zamestnanci == null)
            {
                return NotFound();
            }

            var zamestnanci = await _context.Zamestnanci.FindAsync(id);
            if (zamestnanci == null)
            {
                return NotFound();
            }
            ViewData["IdManazer"] = new SelectList(_context.Zamestnanci, "IdZamestnanec", "Email", zamestnanci.IdManazer);
            ViewData["IdMisto"] = new SelectList(_context.PracovniMista, "IdMisto", "Nazev", zamestnanci.IdMisto);
            ViewData["IdSklad"] = new SelectList(_context.Sklady, "IdSklad", "Nazev", zamestnanci.IdSklad);
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", zamestnanci.IdSupermarket);
            return View(zamestnanci);
        }

        // POST: Zamestnanci/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdZamestnanec,Jmeno,Prijmeni,TelefonniCislo,Email,Mzda,IdSupermarket,IdManazer,IdMisto,IdSklad")] Zamestnanci zamestnanci)
        {
            if (id != zamestnanci.IdZamestnanec)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zamestnanci);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZamestnanciExists(zamestnanci.IdZamestnanec))
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
            ViewData["IdManazer"] = new SelectList(_context.Zamestnanci, "IdZamestnanec", "Email", zamestnanci.IdManazer);
            ViewData["IdMisto"] = new SelectList(_context.PracovniMista, "IdMisto", "Nazev", zamestnanci.IdMisto);
            ViewData["IdSklad"] = new SelectList(_context.Sklady, "IdSklad", "Nazev", zamestnanci.IdSklad);
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", zamestnanci.IdSupermarket);
            return View(zamestnanci);
        }

        // GET: Zamestnanci/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Zamestnanci == null)
            {
                return NotFound();
            }

            var zamestnanci = await _context.Zamestnanci
                .Include(z => z.IdManazerNavigation)
                .Include(z => z.IdMistoNavigation)
                .Include(z => z.IdSkladNavigation)
                .Include(z => z.IdSupermarketNavigation)
                .FirstOrDefaultAsync(m => m.IdZamestnanec == id);
            if (zamestnanci == null)
            {
                return NotFound();
            }

            return View(zamestnanci);
        }

        // POST: Zamestnanci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Zamestnanci == null)
            {
                return Problem("Entity set 'ModelContext.Zamestnanci'  is null.");
            }
            var zamestnanci = await _context.Zamestnanci.FindAsync(id);
            if (zamestnanci != null)
            {
                _context.Zamestnanci.Remove(zamestnanci);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZamestnanciExists(decimal id)
        {
          return _context.Zamestnanci.Any(e => e.IdZamestnanec == id);
        }
    }
}
