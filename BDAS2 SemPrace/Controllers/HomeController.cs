using BDAS2_SemPrace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BDAS2_SemPrace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index() { return View(); }

        public IActionResult Data() { return View(); }

        public IActionResult Zbozi()
        {
            var modelContext = _context.Zbozi.Include(z => z.IdKategorieNavigation).Include(z => z.IdZnackaNavigation);
            return PartialView("Zbozi", modelContext.ToList());
        }

        public IActionResult Platby()
        {
            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev");
            return View();
        }   

        [HttpPost]
        public IActionResult Platby(DateTime start, DateTime end, int id)
        {
            DataSet dataset = new DataSet();
            var connStr = "Data Source=(description=(address_list=(address = (protocol = TCP)(host = fei-sql3.upceucebny.cz)(port = 1521)))(connect_data=(service_name=BDAS.UPCEUCEBNY.CZ))\n);User ID=ST64102;Password=j8ex765gh;Persist Security Info=True";
            using (OracleConnection objConn = new OracleConnection(connStr))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = objConn;
                cmd.CommandText = "platby_v_datech";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_zacatek", OracleDbType.Date).Value = start;
                cmd.Parameters.Add("p_konec", OracleDbType.Date).Value = end;
                cmd.Parameters.Add("p_id_pobocka", OracleDbType.Decimal).Value = id;
                cmd.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    cmd.ExecuteNonQuery();
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dataset);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Exception: {0}", ex.ToString());
                }
                objConn.Close();
            }
            var myData = dataset.Tables[0].AsEnumerable().Select(r => new Platby
            {
                IdPlatba = r.Field<int>("id_platba"),
                Datum = r.Field<DateTime>("datum"),
                Castka = (int)r.Field<decimal>("castka"),
                Typ = r.Field<string>("typ"),
                CisloKarty = r.Field<long?>("cislo_karty"),
                IdSupermarket = (int)r.Field<decimal>("id_supermarket"),
                IdZakaznik = r.Field<int>("id_zakaznik"),
                IdZakaznikNavigation = _context.Zakaznici.Find(r.Field<int>("id_zakaznik"))
            });

            ViewData["IdSupermarket"] = new SelectList(_context.Supermarkety, "IdSupermarket", "Nazev");

            return View(myData.ToList());
        }
    }
}
