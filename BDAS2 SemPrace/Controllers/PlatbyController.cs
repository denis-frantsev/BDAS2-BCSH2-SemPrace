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
    public class PlatbyController : Controller
    {
        private readonly ModelContext _context;

        public PlatbyController(ModelContext context)
        {
            _context = context;
        }

        // GET: Platby
        public async Task<IActionResult> Index()
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();

            var modelContext = _context.Platby.Include(p => p.IdSupermarketNavigation).Include(p => p.IdZakaznikNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Platby/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Platby == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var platby = await _context.Platby
                .Include(p => p.IdSupermarketNavigation)
                .Include(p => p.IdZakaznikNavigation)
                .FirstOrDefaultAsync(m => m.IdPlatba == id);
            if (platby == null)
            {
                return NotFound();
            }

            return View(platby);
        }

        // GET: Platby/Create
        public IActionResult Create()
        {
            if (!ModelContext.HasAdminRights())
                return NotFound();
            //ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev");
            //ViewData["IdZakaznik"] = new SelectList(_context.Zakaznici, "IdZakaznik", "Jmeno");
            return View();
        }

        // POST: Platby/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPlatba,Datum,Castka,Typ,CisloKarty,IdZakaznik,IdSupermarket")] Platby platby)
        {
            if (ModelState.IsValid)
            {
                OracleParameter datum = new() { ParameterName = "p_datum", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Date, Value = platby.Datum };
                OracleParameter castka = new() { ParameterName = "p_castka", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = platby.Castka };
                OracleParameter typ = new() { ParameterName = "p_typ", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = platby.Typ };
                OracleParameter cislo_karty = new() { ParameterName = "p_cislo_karty", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = platby.CisloKarty };
                OracleParameter id_zakaznik = new() { ParameterName = "p_id_zakaznik", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = platby.IdZakaznik };
                OracleParameter id_supermarket = new() { ParameterName = "p_id_supermarket", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = platby.IdSupermarket };

                await _context.Database.ExecuteSqlRawAsync("BEGIN platby_pkg.platby_insert(:p_datum, :p_castka, :p_typ, :p_cislo_karty, :p_id_zakaznik, :p_id_supermarket); END;",
                    datum, castka, typ, cislo_karty, id_zakaznik, id_supermarket);

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", platby.IdSupermarket);
            ViewData["IdZakaznik"] = new SelectList(_context.Zakaznici, "IdZakaznik", "Jmeno", platby.IdZakaznik);
            return View(platby);
        }

        // GET: Platby/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Platby == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var platby = await _context.Platby.FindAsync(id);
            if (platby == null)
            {
                return NotFound();
            }
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", platby.IdSupermarket);
            ViewData["IdZakaznik"] = new SelectList(_context.Zakaznici, "IdZakaznik", "Jmeno", platby.IdZakaznik);
            return View(platby);
        }

        // POST: Platby/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPlatba,Datum,Castka,Typ,CisloKarty,IdZakaznik,IdSupermarket")] Platby platby)
        {
            if (id != platby.IdPlatba)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id };
                    OracleParameter datum = new() { ParameterName = "p_datum", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Date, Value = platby.Datum };
                    OracleParameter castka = new() { ParameterName = "p_castka", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = platby.Castka };
                    OracleParameter typ = new() { ParameterName = "p_typ", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = platby.Typ };
                    OracleParameter cislo_karty = new() { ParameterName = "p_cislo_karty", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = platby.CisloKarty };
                    OracleParameter id_zakaznik = new() { ParameterName = "p_id_zakaznik", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = platby.IdZakaznik };
                    OracleParameter id_supermarket = new() { ParameterName = "p_id_supermarket", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = platby.IdSupermarket };

                    await _context.Database.ExecuteSqlRawAsync("BEGIN platby_pkg.platby_update(:p_id, :p_datum, :p_castka, :p_typ, :p_cislo_karty, :p_id_zakaznik, :p_id_supermarket); END;", p_id, datum, castka, typ, cislo_karty, id_zakaznik, id_supermarket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatbyExists(platby.IdPlatba))
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
            return View(platby);
        }

        // GET: Platby/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Platby == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var platby = await _context.Platby
                .Include(p => p.IdSupermarketNavigation)
                .Include(p => p.IdZakaznikNavigation)
                .FirstOrDefaultAsync(m => m.IdPlatba == id);
            if (platby == null)
            {
                return NotFound();
            }

            return View(platby);
        }

        // POST: Platby/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Platby == null)
            {
                return Problem("Entity set 'ModelContext.Platby'  is null.");
            }
            OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id };
            await _context.Database.ExecuteSqlRawAsync("BEGIN platby_pkg.platby_delete(:p_id); END;", p_id);

            return RedirectToAction(nameof(Index));
        }

        private bool PlatbyExists(long id)
        {
          return _context.Platby.Any(e => e.IdPlatba == id);
        }
    }
}
