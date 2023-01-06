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
    public class ObrazkyController : Controller
    {
        private readonly ModelContext _context;

        public ObrazkyController(ModelContext context)
        {
            _context = context;
        }

        // GET: Obrazky
        public async Task<IActionResult> Index()
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();

            return View(await _context.Obrazky.ToListAsync());
        }

        // GET: Obrazky/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Obrazky == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var obrazky = await _context.Obrazky
                .FirstOrDefaultAsync(m => m.IdObrazek == id);
            if (obrazky == null)
            {
                return NotFound();
            }

            return View(obrazky);
        }

        // GET: Obrazky/Create
        public IActionResult Create()
        {
            if (!ModelContext.HasAdminRights())
                return NotFound();
            ViewData["IdObrazek"] = new SelectList(_context.Adresy, "IdObrazek", "Popis");
            return View();
        }

        // POST: Obrazky/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdObrazek,Data,Popis,Nazev,Pripona")] Obrazky obrazky)
        {
            if (ModelState.IsValid)
            {
                OracleParameter data = new() { ParameterName = "p_data", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Blob, Value = obrazky.Data };
                OracleParameter popis = new() { ParameterName = "p_popis", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = obrazky.Popis };
                OracleParameter nazev = new() { ParameterName = "p_nazev", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = obrazky.Nazev };
                OracleParameter pripona = new() { ParameterName = "p_pripona", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = obrazky.Pripona };

                await _context.Database.ExecuteSqlRawAsync("BEGIN obrazky_pkg.obrazek_insert(:p_data, :p_popis, :p_nazev, :p_pripona); END;", data, popis, nazev, pripona);

                return RedirectToAction(nameof(Index));
            }
            return View(obrazky);
        }

        // GET: Obrazky/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Obrazky == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var obrazky = await _context.Obrazky.FindAsync(id);
            if (obrazky == null)
            {
                return NotFound();
            }
            return View(obrazky);
        }

        // POST: Obrazky/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdObrazek,Data,Popis,Nazev,Pripona")] Obrazky obrazky)
        {
            if (id != obrazky.IdObrazek)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id };
                    OracleParameter data = new() { ParameterName = "p_data", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Blob, Value = obrazky.Data };
                    OracleParameter popis = new() { ParameterName = "p_popis", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = obrazky.Popis };
                    OracleParameter nazev = new() { ParameterName = "p_nazev", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = obrazky.Nazev };
                    OracleParameter pripona = new() { ParameterName = "p_pripona", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = obrazky.Pripona };

                    await _context.Database.ExecuteSqlRawAsync("BEGIN obrazky_pkg.obrazek_update(:p_id, :p_data, :p_popis, :p_nazev, :p_pripona); END;", p_id, data, popis, nazev, pripona);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObrazkyExists(obrazky.IdObrazek))
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
            return View(obrazky);
        }

        // GET: Obrazky/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Obrazky == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var obrazky = await _context.Obrazky
                .FirstOrDefaultAsync(m => m.IdObrazek == id);
            if (obrazky == null)
            {
                return NotFound();
            }

            return View(obrazky);
        }

        // POST: Obrazky/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Obrazky == null)
            {
                return Problem("Entity set 'ModelContext.Obrazky'  is null.");
            }

            OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id };
            await _context.Database.ExecuteSqlRawAsync("BEGIN obrazky_pkg.obrazek_delete(:p_id); END;", p_id);

            return RedirectToAction(nameof(Index));
        }

        private bool ObrazkyExists(int id)
        {
          return _context.Obrazky.Any(e => e.IdObrazek == id);
        }
    }
}
