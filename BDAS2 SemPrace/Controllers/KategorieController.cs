using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BDAS2_SemPrace.Models;
using Oracle.ManagedDataAccess.Client;
using System.Web.Helpers;

namespace BDAS2_SemPrace.Controllers
{
    public class KategorieController : Controller
    {
        private readonly ModelContext _context;

        public KategorieController(ModelContext context)
        {
            _context = context;
        }

        // GET: Kategorie
        public async Task<IActionResult> Index()
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();

            return View(await _context.Kategorie.ToListAsync());
        }

        // GET: Kategorie/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Kategorie == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var kategorie = await _context.Kategorie
                .FirstOrDefaultAsync(m => m.IdKategorie == id);
            if (kategorie == null)
            {
                return NotFound();
            }

            return View(kategorie);
        }

        // GET: Kategorie/Create
        public IActionResult Create()
        {
            if (!ModelContext.HasAdminRights())
                return NotFound();
            return View();
        }

        // POST: Kategorie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKategorie,Nazev,Popis")] Kategorie kategorie)
        {
            if (ModelState.IsValid)
            {
                OracleParameter nazev = new() { ParameterName = "p_nazev", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = kategorie.Nazev };
                OracleParameter popis = new() { ParameterName = "p_popis", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = kategorie.Popis };

                await _context.Database.ExecuteSqlRawAsync("BEGIN kategorie_pkg.kategorie_insert(:p_nazev, :p_popis); END;", nazev, popis);

                return RedirectToAction(nameof(Index));
            }
            return View(kategorie);
        }

        // GET: Kategorie/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Kategorie == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var kategorie = await _context.Kategorie.FindAsync(id);
            if (kategorie == null)
            {
                return NotFound();
            }
            return View(kategorie);
        }

        // POST: Kategorie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdKategorie,Nazev,Popis")] Kategorie kategorie)
        {
            if (id != kategorie.IdKategorie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id, };
                    OracleParameter nazev = new() { ParameterName = "p_nazev", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = kategorie.Nazev };
                    OracleParameter popis = new() { ParameterName = "p_popis", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = kategorie.Popis };

                    await _context.Database.ExecuteSqlRawAsync("BEGIN kategorie_pkg.kategorie_update(:p_id, :p_nazev, :p_popis); END;", p_id, nazev, popis);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategorieExists(kategorie.IdKategorie))
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
            return View(kategorie);
        }

        // GET: Kategorie/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Kategorie == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var kategorie = await _context.Kategorie
                .FirstOrDefaultAsync(m => m.IdKategorie == id);
            if (kategorie == null)
            {
                return NotFound();
            }

            return View(kategorie);
        }

        // POST: Kategorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Kategorie == null)
            {
                return Problem("Entity set 'ModelContext.Kategorie'  is null.");
            }

            OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id, };
            await _context.Database.ExecuteSqlRawAsync("BEGIN kategorie_pkg.kategorie_delete(:p_id); END;", p_id);

            return RedirectToAction(nameof(Index));
        }

        private bool KategorieExists(short id)
        {
          return _context.Kategorie.Any(e => e.IdKategorie == id);
        }
    }
}
