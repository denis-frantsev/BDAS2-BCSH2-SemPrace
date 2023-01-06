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
    public class LogTableController : Controller
    {
        private readonly ModelContext _context;

        public LogTableController(ModelContext context)
        {
            _context = context;
        }

        // GET: LogTable
        public async Task<IActionResult> Index()
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();

            return View(await _context.LogTable.ToListAsync());
        }

        // GET: LogTable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LogTable == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var logTable = await _context.LogTable
                .FirstOrDefaultAsync(m => m.IdZaznam == id);
            if (logTable == null)
            {
                return NotFound();
            }

            return View(logTable);
        }

        // GET: LogTable/Create
        public IActionResult Create()
        {
            if (!ModelContext.HasAdminRights())
                return NotFound();
            return View();
        }

        // POST: LogTable/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZaznam,Tabulka,Operace,Cas")] LogTable logTable)
        {
            if (ModelState.IsValid)
            {
                OracleParameter tabulka = new() { ParameterName = "p_tabulka", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = logTable.Tabulka };
                OracleParameter operace = new() { ParameterName = "p_operace", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = logTable.Operace };
                OracleParameter cas = new() { ParameterName = "p_cas", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.TimeStamp, Value = logTable.Cas };

                await _context.Database.ExecuteSqlRawAsync("BEGIN log_pkg.log_insert(:p_tabulka, :p_operace, :p_cas); END;", tabulka, operace, cas);

                return RedirectToAction(nameof(Index));
            }
            return View(logTable);
        }

        // GET: LogTable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LogTable == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var logTable = await _context.LogTable.FindAsync(id);
            if (logTable == null)
            {
                return NotFound();
            }
            return View(logTable);
        }

        // POST: LogTable/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdZaznam,Tabulka,Operace,Cas")] LogTable logTable)
        {
            if (id != logTable.IdZaznam)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id, };
                    OracleParameter tabulka = new() { ParameterName = "p_tabulka", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = logTable.Tabulka };
                    OracleParameter operace = new() { ParameterName = "p_operace", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = logTable.Operace };
                    OracleParameter cas = new() { ParameterName = "p_cas", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.TimeStamp, Value = logTable.Cas };

                    await _context.Database.ExecuteSqlRawAsync("BEGIN log_pkg.log_update(:p_id, :p_tabulka, :p_operace, :p_cas); END;", p_id, tabulka, operace, cas);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogTableExists(logTable.IdZaznam))
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
            return View(logTable);
        }

        // GET: LogTable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LogTable == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var logTable = await _context.LogTable
                .FirstOrDefaultAsync(m => m.IdZaznam == id);
            if (logTable == null)
            {
                return NotFound();
            }

            return View(logTable);
        }

        // POST: LogTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LogTable == null)
            {
                return Problem("Entity set 'ModelContext.LogTable'  is null.");
            }
            OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id, };
            await _context.Database.ExecuteSqlRawAsync("BEGIN log_pkg.log_delete(:p_id); END;", p_id);

            return RedirectToAction(nameof(Index));
        }

        private bool LogTableExists(int id)
        {
            return _context.LogTable.Any(e => e.IdZaznam == id);
        }
    }
}
