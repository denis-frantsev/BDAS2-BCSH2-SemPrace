using BDAS2_SemPrace.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAS2_SemPrace.Controllers
{
    public class ZakaznikController : Controller
    {
        private readonly IZakaznik _zakaznici;

        public ZakaznikController(IZakaznik zakaznici)
        {
            _zakaznici = zakaznici;
        }

        public ViewResult List() {
            ViewBag.Zakaznik = "Some new";
            var zakaznici = _zakaznici.Zakaznici;
            return View(zakaznici);
        }
    }
}
