﻿using System;
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
    public class ZakazniciController : Controller
    {
        private readonly ModelContext _context;

        public ZakazniciController(ModelContext context)
        {
            _context = context;
        }

        // GET: Zakaznici
        [HttpGet]
        public IActionResult Index(string searchString)
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();

            var zakaznici = _context.Zakaznici.Select(s => s).ToList().Where(s => s == s);

            if (!string.IsNullOrEmpty(searchString))
                zakaznici = zakaznici.Where(s => s.FullName.ToLower().Contains(searchString.ToLower()));
            return View(zakaznici);
        }

        // GET: Zakaznici/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zakaznici == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var zakaznici = await _context.Zakaznici
                .FirstOrDefaultAsync(m => m.IdZakaznik == id);
            if (zakaznici == null)
            {
                return NotFound();
            }

            return View(zakaznici);
        }

        // GET: Zakaznici/Create 7 
        public IActionResult Create()
        {
            if (!ModelContext.HasAdminRights())
                return NotFound();
            return View();
        }

        // POST: Zakaznici/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZakaznik,Jmeno,Prijmeni,TelefonniCislo,Email")] Zakaznici zakaznici)
        {
            if (ModelState.IsValid)
            {
                OracleParameter jmeno = new() { ParameterName = "p_jmeno", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zakaznici.Jmeno };
                OracleParameter prijmeni = new() { ParameterName = "p_prijmeni", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zakaznici.Prijmeni };
                OracleParameter telefonniCislo = new() { ParameterName = "p_telefonni_cislo", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zakaznici.TelefonniCislo };
                OracleParameter email = new() { ParameterName = "p_email", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zakaznici.Email };

                await _context.Database.ExecuteSqlRawAsync("BEGIN zakaznici_pkg.zakaznik_insert(:p_jmeno, :p_prijmeni, :p_telefonni_cislo, :p_email); END;", jmeno, prijmeni, telefonniCislo, email);

                return RedirectToAction(nameof(Index));
            }
            return View(zakaznici);
        }

        // GET: Zakaznici/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zakaznici == null || (!ModelContext.HasAdminRights() && !_context.IsUser(id)))
            {
                return NotFound();
            }

            var zakaznici = await _context.Zakaznici.FindAsync(id);
            if (zakaznici == null)
            {
                return NotFound();
            }
            return View(zakaznici);
        }

        // POST: Zakaznici/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdZakaznik,Jmeno,Prijmeni,TelefonniCislo,Email")] Zakaznici zakaznici)
        {
            if (id != zakaznici.IdZakaznik)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id, };
                    OracleParameter jmeno = new() { ParameterName = "p_jmeno", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zakaznici.Jmeno };
                    OracleParameter prijmeni = new() { ParameterName = "p_prijmeni", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zakaznici.Prijmeni };
                    OracleParameter telefonniCislo = new() { ParameterName = "p_telefonni_cislo", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zakaznici.TelefonniCislo };
                    OracleParameter email = new() { ParameterName = "p_email", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zakaznici.Email };

                    await _context.Database.ExecuteSqlRawAsync("BEGIN zakaznici_pkg.zakaznik_update(:p_id, :p_jmeno, :p_prijmeni, :p_telefonni_cislo, :p_email); END;", p_id, jmeno, prijmeni, telefonniCislo, email);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZakazniciExists(zakaznici.IdZakaznik))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (ModelContext.HasAdminRights())
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction("MyAccount", "Account");
            }
            return View(zakaznici);
        }

        // GET: Zakaznici/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zakaznici == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var zakaznici = await _context.Zakaznici
                .FirstOrDefaultAsync(m => m.IdZakaznik == id);
            if (zakaznici == null)
            {
                return NotFound();
            }

            return View(zakaznici);
        }

        // POST: Zakaznici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zakaznici == null)
            {
                return Problem("Entity set 'ModelContext.Zakaznici'  is null.");
            }

            OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id, };
            await _context.Database.ExecuteSqlRawAsync("BEGIN zakaznici_pkg.zakaznik_delete(:p_id); END;", p_id);
            return RedirectToAction(nameof(Index));
        }

        private bool ZakazniciExists(int id)
        {
            return _context.Zakaznici.Any(e => e.IdZakaznik == id);
        }
    }
}
