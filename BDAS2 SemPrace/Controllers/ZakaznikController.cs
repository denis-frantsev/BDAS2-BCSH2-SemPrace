using BDAS2_SemPrace.Data.Interfaces;
using BDAS2_SemPrace.ViewModels;
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
            //ZakazniciListViewModel obj = new ZakazniciListViewModel();
            //obj.VsichniZakaznici = _zakaznici.Zakaznici;
            ViewBag.Title = "Zákazníci";
            return View(_zakaznici.Zakaznici);
        }

       
    }
}
