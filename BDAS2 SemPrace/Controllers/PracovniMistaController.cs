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
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();

            return View(await _context.PracovniMista.ToListAsync());
        }

        // GET: PracovniMista/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PracovniMista == null || !ModelContext.HasAdminRights())
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
            if (!ModelContext.HasAdminRights())
                return NotFound();
            return View();
        }

        // POST: PracovniMista/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMisto,Nazev,Popis,MinPlat")] PracovniMista pracovniMista)
        {
            if (ModelState.IsValid)
            {
                OracleParameter id_misto = new() { ParameterName = "p_id_misto", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = pracovniMista.IdMisto };
                OracleParameter nazev = new() { ParameterName = "p_nazev", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = pracovniMista.Nazev };
                OracleParameter popis = new() { ParameterName = "p_popis", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = pracovniMista.Popis };
                OracleParameter min_plat = new() { ParameterName = "p_min_plat", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = pracovniMista.MinPlat };

                await _context.Database.ExecuteSqlRawAsync("BEGIN pracovni_mista_pkg.pracovni_mista_insert(:p_nazev, :p_popis, :p_min_plat); END;", nazev, popis, min_plat);

                return RedirectToAction(nameof(Index));
            }
            return View(pracovniMista);
        }

        // GET: PracovniMista/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PracovniMista == null || !ModelContext.HasAdminRights())
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMisto,Nazev,Popis,MinPlat")] PracovniMista pracovniMista)
        {
            if (id != pracovniMista.IdMisto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id, };
                    OracleParameter nazev = new() { ParameterName = "p_nazev", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = pracovniMista.Nazev };
                    OracleParameter popis = new() { ParameterName = "p_popis", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = pracovniMista.Popis };
                    OracleParameter min_plat = new() { ParameterName = "p_min_plat", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = pracovniMista.MinPlat };

                    await _context.Database.ExecuteSqlRawAsync("BEGIN pracovni_mista_pkg.pracovni_mista_update(:p_id, :p_nazev, :p_popis, :p_min_plat); END;", p_id, nazev, popis, min_plat);

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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PracovniMista == null || !ModelContext.HasAdminRights())
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PracovniMista == null)
            {
                return Problem("Entity set 'ModelContext.PracovniMista'  is null.");
            }
            OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id, };
            await _context.Database.ExecuteSqlRawAsync("BEGIN pracovni_mista_pkg.pracovni_mista_delete(:p_id); END;", p_id);

            return RedirectToAction(nameof(Index));
        }

        private bool PracovniMistaExists(int id)
        {
          return _context.PracovniMista.Any(e => e.IdMisto == id);
        }
    }
}
