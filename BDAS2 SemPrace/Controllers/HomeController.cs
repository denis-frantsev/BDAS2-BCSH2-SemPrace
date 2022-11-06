using BDAS2_SemPrace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Zbozi()
        {
            var modelContext = _context.Zbozi.Include(z => z.IdKategorieNavigation).Include(z => z.IdZnackaNavigation);
            return View("~/Views/Zbozi/Index.cshtml", await modelContext.ToListAsync());
        }
    }
}
