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
    public class ZamestnanciController : Controller
    {
        private readonly ModelContext _context;

        public ZamestnanciController(ModelContext context)
        {
            _context = context;
        }

        // GET: Zamestnanci
        [HttpGet]
        public IActionResult Index(int? manazer, int? misto, int? sklad, int? supermarket, string searchString)
        {
            if (ModelContext.User.Role == Role.GHOST || ModelContext.User.Role == Role.REGISTERED)
                return NotFound();
            var zamestnanci = _context.Zamestnanci.Include(z => z.IdManazerNavigation).Include(z => z.IdMistoNavigation).Include(z => z.IdSkladNavigation).Include(z => z.IdSupermarketNavigation).Select(s => s).ToList().Where(s => s == s);
            ViewBag.manazer = new SelectList(_context.Zamestnanci.Where(m => m.IdManazer == null), "IdZamestnanec", "Email");
            ViewBag.misto = new SelectList(_context.PracovniMista, "IdMisto", "Nazev");
            ViewBag.supermarket = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev");
            ViewBag.sklad = new SelectList(_context.Sklady, "IdSklad", "Nazev");

            if (!string.IsNullOrEmpty(searchString))
                zamestnanci = zamestnanci.Where(s => s.FullName.ToLower().Contains(searchString.ToLower()));
            if (supermarket != null)
                zamestnanci = zamestnanci.Where(x => x.IdSupermarketNavigation != null && x.IdSupermarket == supermarket);
            if (manazer != null)
                zamestnanci = zamestnanci.Where(x => x.IdManazerNavigation != null && x.IdManazer == manazer);
            if (misto != null)
                zamestnanci = zamestnanci.Where(x => x.IdMisto == misto);
            if (sklad != null)
                zamestnanci = zamestnanci.Where(x => x.IdSklad != null && x.IdSklad == sklad);
            
            //var modelContext = _context.Zamestnanci.Include(z => z.IdManazerNavigation).Include(z => z.IdMistoNavigation).Include(z => z.IdSkladNavigation).Include(z => z.IdSupermarketNavigation);
            return View(zamestnanci);
        }

        // GET: Zamestnanci/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zamestnanci == null)
            {
                return NotFound();
            }

            var zamestnanci = await _context.Zamestnanci
                .Include(z => z.IdManazerNavigation)
                .Include(z => z.IdMistoNavigation)
                .Include(z => z.IdSkladNavigation)
                .Include(z => z.IdSupermarketNavigation)
                .FirstOrDefaultAsync(m => m.IdZamestnanec == id);
            if (zamestnanci == null)
            {
                return NotFound();
            }

            return View(zamestnanci);
        }

        // GET: Zamestnanci/Create
        public IActionResult Create()
        {
            if (!ModelContext.HasAdminRights())
                return NotFound();
            ViewData["IdManazer"] = new SelectList(_context.Zamestnanci, "IdZamestnanec", "Email");
            ViewData["IdMisto"] = new SelectList(_context.PracovniMista, "IdMisto", "Nazev");
            ViewData["IdSklad"] = new SelectList(_context.Sklady, "IdSklad", "Nazev");
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev");
            return View();
        }

        // POST: Zamestnanci/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdZamestnanec,Jmeno,Prijmeni,TelefonniCislo,Email,Mzda,IdSupermarket,IdManazer,IdMisto,IdSklad")] Zamestnanci zamestnanci)
        {
            if (zamestnanci.IdSklad == 0)
                zamestnanci.IdSklad = null;
            if (zamestnanci.IdSupermarket == 0)
                zamestnanci.IdSupermarket = null;
            if (zamestnanci.IdManazer == 0)
                zamestnanci.IdManazer = null;

            if (ModelState.IsValid)
            {
                OracleParameter jmeno = new() { ParameterName = "p_jmeno", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zamestnanci.Jmeno };
                OracleParameter prijmeni = new() { ParameterName = "p_prijmeni", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zamestnanci.Prijmeni };
                OracleParameter telefonniCislo = new() { ParameterName = "p_telefonni_cislo", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zamestnanci.TelefonniCislo };
                OracleParameter email = new() { ParameterName = "p_email", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zamestnanci.Email };
                OracleParameter mzda = new() { ParameterName = "p_mzda", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zamestnanci.Mzda };
                OracleParameter id_supermarket = new() { ParameterName = "p_id_supermarket", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zamestnanci.IdSupermarket };
                OracleParameter id_manazer = new() { ParameterName = "p_id_manazer", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zamestnanci.IdManazer };
                OracleParameter id_misto = new() { ParameterName = "p_id_misto", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zamestnanci.IdMisto };
                OracleParameter id_sklad = new() { ParameterName = "p_id_sklad", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zamestnanci.IdSklad };
                await _context.Database.ExecuteSqlRawAsync("BEGIN zamestnanci_pkg.zamestnanec_insert(:p_jmeno, :p_prijmeni, :p_telefonni_cislo, :p_email, :p_mzda, :p_id_supermarket," +
                    ":p_id_manazer, :p_id_misto, :p_id_sklad); END;", jmeno, prijmeni, telefonniCislo, email, mzda, id_supermarket, id_manazer, id_misto, id_sklad);
                return RedirectToAction(nameof(Index));
            }
            return View(zamestnanci);
        }

        // GET: Zamestnanci/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zamestnanci == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var zamestnanci = await _context.Zamestnanci.FindAsync(id);
            if (zamestnanci == null)
            {
                return NotFound();
            }
            ViewData["IdManazer"] = new SelectList(_context.Zamestnanci, "IdZamestnanec", "Email", zamestnanci.IdManazer);
            ViewData["IdMisto"] = new SelectList(_context.PracovniMista, "IdMisto", "Nazev", zamestnanci.IdMisto);
            ViewData["IdSklad"] = new SelectList(_context.Sklady, "IdSklad", "Nazev", zamestnanci.IdSklad);
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", zamestnanci.IdSupermarket);
            return View(zamestnanci);
        }

        // POST: Zamestnanci/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdZamestnanec,Jmeno,Prijmeni,TelefonniCislo,Email,Mzda,IdSupermarket,IdManazer,IdMisto,IdSklad")] Zamestnanci zamestnanci)
        {
            if (id != zamestnanci.IdZamestnanec)
            {
                return NotFound();
            }

            if (zamestnanci.IdSklad == 0)
                zamestnanci.IdSklad = null;
            if (zamestnanci.IdSupermarket == 0)
                zamestnanci.IdSupermarket = null;
            if(zamestnanci.IdManazer == 0)
                zamestnanci.IdManazer = null;

            if (ModelState.IsValid)
            {
                try
                {
                    OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zamestnanci.IdZamestnanec };
                    OracleParameter jmeno = new() { ParameterName = "p_jmeno", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zamestnanci.Jmeno };
                    OracleParameter prijmeni = new() { ParameterName = "p_prijmeni", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zamestnanci.Prijmeni };
                    OracleParameter telefonniCislo = new() { ParameterName = "p_telefonni_cislo", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zamestnanci.TelefonniCislo };
                    OracleParameter email = new() { ParameterName = "p_email", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Varchar2, Value = zamestnanci.Email };
                    OracleParameter mzda = new() { ParameterName = "p_mzda", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zamestnanci.Mzda };
                    OracleParameter id_supermarket = new() { ParameterName = "p_id_supermarket", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zamestnanci.IdSupermarket };
                    OracleParameter id_manazer = new() { ParameterName = "p_id_manazer", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zamestnanci.IdManazer };
                    OracleParameter id_misto = new() { ParameterName = "p_id_misto", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zamestnanci.IdMisto };
                    OracleParameter id_sklad = new() { ParameterName = "p_id_sklad", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = zamestnanci.IdSklad };

                    await _context.Database.ExecuteSqlRawAsync("BEGIN zamestnanci_pkg.zamestnanec_update(:p_id, :p_jmeno, :p_prijmeni, :p_telefonni_cislo, :p_email, :p_mzda, :p_id_supermarket," +
                                        ":p_id_manazer, :p_id_misto, :p_id_sklad); END;", p_id, jmeno, prijmeni, telefonniCislo, email, mzda, id_supermarket, id_manazer, id_misto, id_sklad);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZamestnanciExists(zamestnanci.IdZamestnanec))
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
            //ViewData["IdManazer"] = new SelectList(_context.Zamestnanci, "IdZamestnanec", "Email", zamestnanci.IdManazer);
            //ViewData["IdMisto"] = new SelectList(_context.PracovniMista, "IdMisto", "Nazev", zamestnanci.IdMisto);
            //ViewData["IdSklad"] = new SelectList(_context.Sklady, "IdSklad", "Nazev", zamestnanci.IdSklad);
            //ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev", zamestnanci.IdSupermarket);
            return View(zamestnanci);
        }

        // GET: Zamestnanci/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zamestnanci == null || !ModelContext.HasAdminRights())
            {
                return NotFound();
            }

            var zamestnanci = await _context.Zamestnanci
                .Include(z => z.IdManazerNavigation)
                .Include(z => z.IdMistoNavigation)
                .Include(z => z.IdSkladNavigation)
                .Include(z => z.IdSupermarketNavigation)
                .FirstOrDefaultAsync(m => m.IdZamestnanec == id);
            if (zamestnanci == null)
            {
                return NotFound();
            }

            return View(zamestnanci);
        }

        // POST: Zamestnanci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zamestnanci == null)
            {
                return Problem("Entity set 'ModelContext.Zamestnanci'  is null.");
            }
            //var zamestnanci = await _context.Zamestnanci.FindAsync(id);
            //if (zamestnanci != null)
            //{
            //    _context.Zamestnanci.Remove(zamestnanci);
            //}
            OracleParameter p_id = new() { ParameterName = "p_id", Direction = System.Data.ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = id, };
            await _context.Database.ExecuteSqlRawAsync("BEGIN zamestnanci_pkg.zamestnanec_delete(:p_id);END;", p_id);

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZamestnanciExists(int id)
        {
            return _context.Zamestnanci.Any(e => e.IdZamestnanec == id);
        }
    }
}
