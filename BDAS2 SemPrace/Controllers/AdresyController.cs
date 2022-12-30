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
    public class AdresyController : Controller
    {
        private readonly ModelContext _context;

        public AdresyController(ModelContext context)
        {
            _context = context;
        }

        // GET: Adresy
        public async Task<IActionResult> Index()
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();

            return View(await _context.Adresy.ToListAsync());
        }

        // GET: Adresy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Adresy == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }
            var adresy = await _context.Adresy
                .FirstOrDefaultAsync(m => m.IdAdresa == id);
            if (adresy == null)
            {
                return NotFound();
            }

            return View(adresy);
        }

        // GET: Adresy/Create
        public IActionResult Create()
        {
            if (!ModelContext.HasAdminRights())
                return NotFound();

            return View();
        }

        // POST: Adresy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAdresa,Ulice,Mesto,Psc")] Adresy adresy)
        {
            if (ModelState.IsValid)
            {
                OracleParameter ulice = new() { ParameterName = "p_ulice", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = adresy.Ulice };
                OracleParameter mesto = new() { ParameterName = "p_mesto", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = adresy.Mesto };
                OracleParameter psc = new() { ParameterName = "p_psc", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = adresy.Psc };

                await _context.Database.ExecuteSqlRawAsync("BEGIN adresy_pkg.adresa_insert(:p_ulice, :p_mesto, :p_psc); END;", ulice, mesto, psc);
                return RedirectToAction(nameof(Index));
            }
            return View(adresy);
        }

        // GET: Adresy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Adresy == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var adresy = await _context.Adresy.FindAsync(id);
            if (adresy == null)
            {
                return NotFound();
            }
            return View(adresy);
        }

        // POST: Adresy/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAdresa,Ulice,Mesto,Psc")] Adresy adresy)
        {
            if (id != adresy.IdAdresa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = adresy.IdAdresa };
                    OracleParameter ulice = new() { ParameterName = "p_ulice", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = adresy.Ulice };
                    OracleParameter mesto = new() { ParameterName = "p_mesto", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = adresy.Mesto };
                    OracleParameter psc = new() { ParameterName = "p_psc", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = adresy.Psc };

                    await _context.Database.ExecuteSqlRawAsync("BEGIN adresy_pkg.adresa_update(:p_id, :p_ulice, :p_mesto, :p_psc); END;", p_id, ulice, mesto, psc);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdresyExists((int)adresy.IdAdresa))
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
            return View(adresy);
        }

        // GET: Adresy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Adresy == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var adresy = await _context.Adresy
                .FirstOrDefaultAsync(m => m.IdAdresa == id);
            if (adresy == null)
            {
                return NotFound();
            }

            return View(adresy);
        }

        // POST: Adresy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Adresy == null)
            {
                return Problem("Entity set 'ModelContext.Adresy'  is null.");
            }
            OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id, };
            await _context.Database.ExecuteSqlRawAsync("BEGIN adresy_pkg.adresa_delete(:p_id); END;", p_id);

            return RedirectToAction(nameof(Index));
        }

        private bool AdresyExists(int id)
        {
          return _context.Adresy.Any(e => e.IdAdresa == id);
        }
    }
}
