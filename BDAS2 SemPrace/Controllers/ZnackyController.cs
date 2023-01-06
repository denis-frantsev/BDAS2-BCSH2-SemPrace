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
    public class ZnackyController : Controller
    {
        private readonly ModelContext _context;

        public ZnackyController(ModelContext context)
        {
            _context = context;
        }

        // GET: Znacky
        public async Task<IActionResult> Index()
        {
              return View(await _context.Znacky.ToListAsync());
        }

        // GET: Znacky/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Znacky == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var znacky = await _context.Znacky
                .FirstOrDefaultAsync(m => m.IdZnacka == id);
            if (znacky == null)
            {
                return NotFound();
            }

            return View(znacky);
        }

        // GET: Znacky/Create
        public IActionResult Create()
        {
            if (!ModelContext.HasAdminRights())
                return NotFound();
            return View();
        }

        // POST: Znacky/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZnacka,Nazev")] Znacky znacky)
        {
            if (ModelState.IsValid)
            {
                OracleParameter nazev = new()
                {
                    ParameterName = "p_nazev",
                    Direction = System.Data.ParameterDirection.Input, 
                    OracleDbType = OracleDbType.Varchar2,
                    Value = znacky.Nazev
                };
                await _context.Database.ExecuteSqlRawAsync("BEGIN znacky_pkg.znacka_insert(:p_nazev); END;", nazev);
                return RedirectToAction(nameof(Index));
            }
            return View(znacky);
        }

        // GET: Znacky/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Znacky == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var znacky = await _context.Znacky.FindAsync(id);
            if (znacky == null)
            {
                return NotFound();
            }
            return View(znacky);
        }

        // POST: Znacky/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdZnacka,Nazev")] Znacky znacky)
        {
            if (id != znacky.IdZnacka)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    OracleParameter ID = new()
                    {
                        ParameterName = "p_id",
                        Direction = System.Data.ParameterDirection.Input,
                        OracleDbType = OracleDbType.Int32,
                        Value = id
                    };
                    OracleParameter nazev = new()
                    {
                        ParameterName = "p_nazev",
                        Direction = System.Data.ParameterDirection.Input,
                        OracleDbType = OracleDbType.Varchar2, 
                        Value = znacky.Nazev
                    };

                    await _context.Database.ExecuteSqlRawAsync("BEGIN znacky_pkg.znacka_update(:p_id,:p_nazev); END;",ID,nazev);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZnackyExists(znacky.IdZnacka))
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
            return View(znacky);
        }

        // GET: Znacky/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Znacky == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var znacky = await _context.Znacky
                .FirstOrDefaultAsync(m => m.IdZnacka == id);
            if (znacky == null)
            {
                return NotFound();
            }

            return View(znacky);
        }

        // POST: Znacky/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Znacky == null)
            {
                return Problem("Entity set 'ModelContext.Znacky'  is null.");
            }

            OracleParameter ID = new()
            {
                ParameterName = "p_id",
                Direction = System.Data.ParameterDirection.Input,
                OracleDbType = OracleDbType.Int32,
                Value = id
            };

            await _context.Database.ExecuteSqlRawAsync("BEGIN znacky_pkg.znacka_delete(:p_id); END;", ID);

            return RedirectToAction(nameof(Index));
        }

        private bool ZnackyExists(short id)
        {
          return _context.Znacky.Any(e => e.IdZnacka == id);
        }
    }
}
