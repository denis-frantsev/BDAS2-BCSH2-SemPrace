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
    public class PokladnyController : Controller
    {
        private readonly ModelContext _context;

        public PokladnyController(ModelContext context)
        {
            _context = context;
        }

        // GET: Pokladny
        public async Task<IActionResult> Index()
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();

            var modelContext = _context.Pokladny.Include(p => p.IdSupermarketNavigation);
            return View(await modelContext.ToListAsync());
        }

        // GET: Pokladny/Details/5
        public async Task<IActionResult> Details(int? supermarketId, int? cisloPokladny)
        {
            if (supermarketId == null || cisloPokladny == null ||_context.Pokladny == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var pokladny = await _context.Pokladny
                .Include(p => p.IdSupermarketNavigation)
                .FirstOrDefaultAsync(m => m.IdSupermarket == supermarketId && m.CisloPokladny == cisloPokladny);
            if (pokladny == null)
            {
                return NotFound();
            }

            return View(pokladny);
        }

        // GET: Pokladny/Create
        public IActionResult Create()
        {
            if (!ModelContext.HasAdminRights())
                return NotFound();
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev");
            return View();
        }

        // POST: Pokladny/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSupermarket,CisloPokladny")] Pokladny pokladny)
        {
            if (ModelState.IsValid)
            {
                OracleParameter id_supermarket = new() { ParameterName = "p_id_supermarket", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = pokladny.IdSupermarket };
                OracleParameter cislo_pokladny = new() { ParameterName = "p_cislo_pokladny", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = pokladny.CisloPokladny };
                
                await _context.Database.ExecuteSqlRawAsync("BEGIN pokladny_pkg.pokladny_insert(:p_id_supermarket, :p_cislo_pokladny); END;", id_supermarket, cislo_pokladny);

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", pokladny.IdSupermarket);
            return View(pokladny);
        }

        // GET: Pokladny/Edit/5
        public async Task<IActionResult> Edit(int? supermarketId, int? cisloPokladny)
        {
            if (supermarketId == null || cisloPokladny == null || _context.Pokladny == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var pokladny = await _context.Pokladny.FindAsync(supermarketId, cisloPokladny);
            pokladny.IdSupermarketNavigation = await _context.Supermarkety.FindAsync(supermarketId);
            if (pokladny == null)
            {
                return NotFound();
            }
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", pokladny.IdSupermarket);
            return View(pokladny);
        }

        // POST: Pokladny/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int supermarketId, int cisloPokladny, [Bind("IdSupermarket,CisloPokladny")] Pokladny pokladny)
        {
            if (cisloPokladny != pokladny.CisloPokladny && supermarketId != pokladny.IdSupermarket)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    OracleParameter id_supermarket = new() { ParameterName = "p_id_supermarket", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = pokladny.IdSupermarket };
                    OracleParameter cislo_pokladny = new() { ParameterName = "p_cislo_pokladny", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = pokladny.CisloPokladny };

                    await _context.Database.ExecuteSqlRawAsync("BEGIN pokladny_pkg.pokladny_update(:p_id_supermarket, :p_cislo_pokladny); END;", id_supermarket, cislo_pokladny);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokladnyExists(pokladny.IdSupermarket, pokladny.CisloPokladny))
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
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", pokladny.IdSupermarket);
            return View(pokladny);
        }

        // GET: Pokladny/Delete/5
        public async Task<IActionResult> Delete(int? supermarketId, int? cisloPokladny)
        {
            if (cisloPokladny == null || _context.Pokladny == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var pokladny = await _context.Pokladny
                .Include(p => p.IdSupermarketNavigation)
                .FirstOrDefaultAsync(m => m.IdSupermarket == supermarketId && m.CisloPokladny == cisloPokladny);
            if (pokladny == null)
            {
                return NotFound();
            }

            return View(pokladny);
        }

        // POST: Pokladny/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("IdSupermarket,CisloPokladny")] Pokladny pokladny)
        {
            if (_context.Pokladny == null)
            {
                return Problem("Entity set 'ModelContext.Pokladny'  is null.");
            }
            OracleParameter id_supermarket = new() { ParameterName = "p_id_supermarket", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = pokladny.IdSupermarket };
            OracleParameter cislo_pokladny = new() { ParameterName = "p_cislo_pokladny", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = pokladny.CisloPokladny };

            await _context.Database.ExecuteSqlRawAsync("BEGIN pokladny_pkg.pokladny_delete(:p_id_supermarket, :p_cislo_pokladny); END;", id_supermarket, cislo_pokladny);

            return RedirectToAction(nameof(Index));
        }

        private bool PokladnyExists(int supermarketId, int cisloPokladny)
        {
          return _context.Pokladny.Any(e => e.IdSupermarket == supermarketId && e.CisloPokladny == cisloPokladny);
        }
    }
}
