using BDAS2_SemPrace.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public ActionResult SignIn([Bind("Permission,Password,Email,ID")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (UserWithEmailExists(user.Email))
                    {
                        if (UserPasswordCorrect(user.Email, user.Password))
                        {
                            var dbUser = _context.Users.Find(user.Email);
                            user.Permision = dbUser.Permision;
                            user.ID = dbUser.ID;
                            ModelContext.User = user;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("Password", "Špatné heslo.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Uživatel s tímto emailem neexistuje");
                        return View();
                    }
                }
                catch (Exception)
                {
                    throw;
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
        public async Task<IActionResult> Register([Bind("Permission,Password,Email,ID")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (UserWithEmailExists(user.Email))
                    {
                        ModelState.AddModelError("Email", "Uživatel s tímto emailem už existuje.");
                        return View();
                    }
                    user.Permision = Permision.REGISTERED;
                    _context.Add(user);
                    ModelContext.User = user;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception)
                {
                    throw;
                }

            }
            return View();
        }

        public IActionResult MyAccount() { return View(); }
         
        public IActionResult LogOut()
        {
            ModelContext.User = new() { Permision = Permision.GHOST };
            return RedirectToAction("Index","Home");
        }

        private bool UserWithEmailExists(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }

        private bool UserPasswordCorrect(string email, string password)
        {
            return _context.Users.Where(e => e.Password == password).Any(e => e.Email == email);
        }
    }

}
