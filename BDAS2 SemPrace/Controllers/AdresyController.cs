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
                _context.Add(adresy);
                await _context.SaveChangesAsync();
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
                    _context.Update(adresy);
                    await _context.SaveChangesAsync();
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
            var adresy = await _context.Adresy.FindAsync(id);
            if (adresy != null)
            {
                _context.Adresy.Remove(adresy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdresyExists(int id)
        {
          return _context.Adresy.Any(e => e.IdAdresa == id);
        }
    }
}
