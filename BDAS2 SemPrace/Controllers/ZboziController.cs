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
    public class ZboziController : Controller
    {
        private readonly ModelContext _context;

        public ZboziController(ModelContext context)
        {
            _context = context;
        }

        // GET: Zbozi
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Zbozi.Include(z => z.IdKategorieNavigation).Include(z => z.IdZnackaNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Zbozi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zbozi == null)
            {
                return NotFound();
            }

            var zbozi = await _context.Zbozi
                .Include(z => z.IdKategorieNavigation)
                .Include(z => z.IdZnackaNavigation)
                .FirstOrDefaultAsync(m => m.IdZbozi == id);
            if (zbozi == null)
            {
                return NotFound();
            }

            return View(zbozi);
        }

        // GET: Zbozi/Create
        public IActionResult Create()
        {
            if (!ModelContext.HasAdminRights())
                return NotFound();
            ViewData["IdKategorie"] = new SelectList(_context.Kategorie, "IdKategorie", "Nazev");
            ViewData["IdZnacka"] = new SelectList(_context.Znacky, "IdZnacka", "Nazev");
            return View();
        }

        // POST: Zbozi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZbozi,KodZbozi,NazevZbozi,IdKategorie,IdZnacka,Popis,Cena,Obrazek")] Zbozi zbozi)
        {
            if (ModelState.IsValid)
            {
                OracleParameter kodZbozi = new() { ParameterName = "p_kod_zbozi", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zbozi.KodZbozi };
                OracleParameter nazevZbozi = new() { ParameterName = "p_nazev_zbozi", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zbozi.NazevZbozi };
                OracleParameter idKategorie = new() { ParameterName = "p_id_kategorie", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zbozi.IdKategorie };
                OracleParameter idZnacka = new() { ParameterName = "p_id_znacka", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zbozi.IdZnacka };
                OracleParameter popis = new() { ParameterName = "p_popis", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zbozi.Popis };
                OracleParameter cena = new() { ParameterName = "p_cena", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zbozi.Cena };
                OracleParameter obrazek = new() { ParameterName = "p_obrazek", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zbozi.Obrazek };

                await _context.Database.ExecuteSqlRawAsync("BEGIN zbozi_pkg.zbozi_insert (:p_kod_zbozi, :p_nazev_zbozi, :p_id_kategorie, :p_id_znacka, :p_popis, :p_cena, :p_obrazek); END;", kodZbozi, nazevZbozi, idKategorie, idZnacka, popis, cena, obrazek);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKategorie"] = new SelectList(_context.Kategorie, "IdKategorie", "Nazev", zbozi.IdKategorie);
            ViewData["IdZnacka"] = new SelectList(_context.Znacky, "IdZnacka", "Nazev", zbozi.IdZnacka);
            return View(zbozi);
        }

        // GET: Zbozi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zbozi == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var zbozi = await _context.Zbozi.FindAsync(id);
            if (zbozi == null)
            {
                return NotFound();
            }
            ViewData["IdKategorie"] = new SelectList(_context.Kategorie, "IdKategorie", "Nazev", zbozi.IdKategorie);
            ViewData["IdZnacka"] = new SelectList(_context.Znacky, "IdZnacka", "Nazev", zbozi.IdZnacka);
            return View(zbozi);
        }

        // POST: Zbozi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdZbozi,KodZbozi,NazevZbozi,IdKategorie,IdZnacka,Popis,Cena,Obrazek")] Zbozi zbozi)
        {
            if (id != zbozi.IdZbozi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id };
                    OracleParameter kodZbozi = new() { ParameterName = "p_kod_zbozi", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zbozi.KodZbozi };
                    OracleParameter nazevZbozi = new() { ParameterName = "p_nazev_zbozi", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zbozi.NazevZbozi };
                    OracleParameter idKategorie = new() { ParameterName = "p_id_kategorie", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zbozi.IdKategorie };
                    OracleParameter idZnacka = new() { ParameterName = "p_id_znacka", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zbozi.IdZnacka };
                    OracleParameter popis = new() { ParameterName = "p_popis", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zbozi.Popis };
                    OracleParameter cena = new() { ParameterName = "p_cena", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zbozi.Cena };
                    OracleParameter obrazek = new() { ParameterName = "p_obrazek", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zbozi.Obrazek };

                    await _context.Database.ExecuteSqlRawAsync("BEGIN zbozi_pkg.zbozi_update (:p_id, :p_kod_zbozi, :p_nazev_zbozi, :p_id_kategorie, :p_id_znacka, :p_popis, :p_cena, :p_obrazek); END;", p_id, kodZbozi, nazevZbozi, idKategorie, idZnacka, popis, cena, obrazek);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZboziExists(zbozi.IdZbozi))
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
            ViewData["IdKategorie"] = new SelectList(_context.Kategorie, "IdKategorie", "Nazev", zbozi.IdKategorie);
            ViewData["IdZnacka"] = new SelectList(_context.Znacky, "IdZnacka", "Nazev", zbozi.IdZnacka);
            return View(zbozi);
        }

        // GET: Zbozi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zbozi == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var zbozi = await _context.Zbozi
                .Include(z => z.IdKategorieNavigation)
                .Include(z => z.IdZnackaNavigation)
                .FirstOrDefaultAsync(m => m.IdZbozi == id);
            if (zbozi == null)
            {
                return NotFound();
            }

            return View(zbozi);
        }

        // POST: Zbozi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zbozi == null)
            {
                return Problem("Entity set 'ModelContext.Zbozi'  is null.");
            }

            OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id };
            await _context.Database.ExecuteSqlRawAsync("BEGIN zbozi_pkg.zbozi_delete(:p_id); END;", p_id);

            return RedirectToAction(nameof(Index));
        }

        private bool ZboziExists(int id)
        {
            return _context.Zbozi.Any(e => e.IdZbozi == id);
        }
    }
}
