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
        [HttpGet]
        public IActionResult Index(int? manazer, int? misto, int? sklad, int? supermarket, string searchString)
        {
            var zamestnanci = _context.Zamestnanci.Include(z => z.IdManazerNavigation).Include(z => z.IdMistoNavigation).Include(z => z.IdSkladNavigation).Include(z => z.IdSupermarketNavigation).Select(s => s).ToList().Where(s => s == s);
            ViewBag.manazer = new SelectList(_context.Zamestnanci.Where(m => m.IdManazer == null), "IdZamestnanec", "Email");
            ViewBag.misto = new SelectList(_context.PracovniMista, "IdMisto", "Nazev");
            ViewBag.supermarket = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev");
            ViewBag.sklad = new SelectList(_context.Sklady, "IdSklad", "Nazev");

            if (!string.IsNullOrEmpty(searchString))
                zamestnanci = zamestnanci.Where(s => s.FullName.ToLower().Contains(searchString.ToLower()));
            if (supermarket != null)
                zamestnanci = zamestnanci.Where(x => x.IdSupermarketNavigation != null && x.IdSupermarket == supermarket);
            if (manazer != null)
                zamestnanci = zamestnanci.Where(x => x.IdManazerNavigation != null && x.IdManazer == manazer);
            if (misto != null)
                zamestnanci = zamestnanci.Where(x => x.IdMisto == misto);
            if (sklad != null)
                zamestnanci = zamestnanci.Where(x => x.IdSklad != null && x.IdSklad == sklad);
            
            //var modelContext = _context.Zamestnanci.Include(z => z.IdManazerNavigation).Include(z => z.IdMistoNavigation).Include(z => z.IdSkladNavigation).Include(z => z.IdSupermarketNavigation);
            return View(zamestnanci);
        }

        // GET: Zamestnanci/Details/5
        public async Task<IActionResult> Details(int? id)
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
            if (!ModelContext.HasAdminRights())
                return NotFound();
            ViewData["IdManazer"] = new SelectList(_context.Zamestnanci, "IdZamestnanec", "Email");
            ViewData["IdMisto"] = new SelectList(_context.PracovniMista, "IdMisto", "Nazev");
            ViewData["IdSklad"] = new SelectList(_context.Sklady, "IdSklad", "Nazev");
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev");
            return View();
        }

        // POST: Zamestnanci/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZamestnanec,Jmeno,Prijmeni,TelefonniCislo,Email,Mzda,IdSupermarket,IdManazer,IdMisto,IdSklad")] Zamestnanci zamestnanci)
        {
            if (zamestnanci.IdSklad == 0)
                zamestnanci.IdSklad = null;
            if (zamestnanci.IdSupermarket == 0)
                zamestnanci.IdSupermarket = null;
            if (zamestnanci.IdManazer == 0)
                zamestnanci.IdManazer = null;

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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zamestnanci == null || !ModelContext.HasAdminRights())
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdZamestnanec,Jmeno,Prijmeni,TelefonniCislo,Email,Mzda,IdSupermarket,IdManazer,IdMisto,IdSklad")] Zamestnanci zamestnanci)
        {
            if (id != zamestnanci.IdZamestnanec)
            {
                return NotFound();
            }

            if (zamestnanci.IdSklad == 0)
                zamestnanci.IdSklad = null;
            if (zamestnanci.IdSupermarket == 0)
                zamestnanci.IdSupermarket = null;
            if(zamestnanci.IdManazer == 0)
                zamestnanci.IdManazer = null;

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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zamestnanci == null || !ModelContext.HasAdminRights())
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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

        private bool ZamestnanciExists(int id)
        {
            return _context.Zamestnanci.Any(e => e.IdZamestnanec == id);
        }
    }
}
