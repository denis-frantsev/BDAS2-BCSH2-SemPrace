using BDAS2_SemPrace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BDAS2_SemPrace.Controllers
{
    public class AccountController : Controller
    {
        private readonly ModelContext _context;

        public AccountController(ModelContext context)
        {
            _context = context;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn([Bind("Permission,Password,Email")]User user)
        {
            if (ModelState.IsValid)
            {
                if (UserWithEmailExists(user.Email))
                {
                    _context.User = user;
                    return RedirectToAction("Index", "Home", user);
                }
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Password,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Permision = Permision.REGISTERED;
                _context.Add(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home", user);
        }

        private bool UserWithEmailExists(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }

    }

}
