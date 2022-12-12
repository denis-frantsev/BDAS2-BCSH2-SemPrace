using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BDAS2_SemPrace.Models;
using Oracle.ManagedDataAccess.Client;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BDAS2_SemPrace.Controllers
{
    public class UseryController : Controller
    {
        private readonly ModelContext _context;

        public UseryController(ModelContext context)
        {
            _context = context;
        }

        // GET: Usery
        public async Task<IActionResult> Index()
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();
            var users = await _context.Users.ToListAsync();
            users.ForEach(async u => {
                if (_context.Zamestnanci.Any(z=>z.Email == u.Email)) {
                    u.ZamestnanecNav = await _context.Zamestnanci.FirstOrDefaultAsync(z=> z.Email == u.Email);
                }
                else
                {
                    u.ZakaznikNav = await _context.Zakaznici.FirstOrDefaultAsync(z => z.Email == u.Email);
                }
            });
            return View(users);
        }

        // GET: Usery/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Users == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Email == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Usery/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Users == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Usery/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Role,Password,Email,ProfilePic")] User user, IFormFile file)
        {
            if (id != user.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IFormFile image = user.ProfilePic;
                    if (image != null)
                    {
                        using var stream = new MemoryStream();
                        await image.CopyToAsync(stream);
                        user.IdObrazekNavigation.Data = stream.ToArray();
                        user.IdObrazekNavigation.Popis = "Profilový obrázek";
                        await _context.Obrazky.AddAsync(user.IdObrazekNavigation);
                    }
                    _context.Update(user);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Email))
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
            return View(user);
        }

        // GET: Usery/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Users == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Email == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Usery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ModelContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                OracleParameter p_id = new OracleParameter() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = id, };
                await _context.Database.ExecuteSqlRawAsync("BEGIN DELETE_USER(:p_id); END;", p_id);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
          return _context.Users.Any(e => e.Email == id);
        }
    }
}
