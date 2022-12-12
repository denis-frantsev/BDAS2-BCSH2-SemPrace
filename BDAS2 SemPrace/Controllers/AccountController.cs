using BDAS2_SemPrace.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
                        var encodedPassword = MD5HashedPassword(user.Password);
                        if (UserPasswordCorrect(user.Email, encodedPassword))
                        {
                            var dbUser = _context.Users.Find(user.Email);
                            user.Role = dbUser.Role;
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
                    return NotFound();
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
                    user.Role = Role.REGISTERED;
                    _context.Add(user);
                    ModelContext.User = user;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception)
                {
                    return NotFound();
                }

            }
            return View();
        }

        public async Task<IActionResult> MyAccount()
        {
            var user = await _context.Users.FindAsync(ModelContext.User.Email);
            if (user.Role == Role.GHOST)
                return NotFound();
            else if (user.Role == Role.REGISTERED)
                return View("MyAccountCustomer", new UserCustomerViewModel(user, _context));
            else
                return View("MyAccountEmployee", new UserEmployeeViewModel(user, _context));
        }

        public IActionResult LogOut()
        {
            ModelContext.User = new() { Role = Role.GHOST };
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Emulate(string email)
        {
            var emulator = ModelContext.User;
            ModelContext.User = await _context.Users.FindAsync(email);
            ModelContext.User.Emulator = emulator;
            ModelContext.User.IsEmulated = true;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult StopEmulating()
        {
            var user = ModelContext.User;
            user.IsEmulated = false;
            ModelContext.User = user.Emulator;
            user.Emulator = null;

            return RedirectToAction("Index", "Usery");
        }

        public IActionResult AddProfilePic() => View();

        [HttpPost("Account")]
        public async Task<IActionResult> AddProfilePic(IFormFile image)
        {
            if (ModelContext.User.Role == Role.GHOST)
                return NotFound();

            var user = await _context.Users.FindAsync(ModelContext.User.Email);

            if (image != null)
            {
                using var stream = new MemoryStream();
                await image.CopyToAsync(stream);
                Obrazky obrazek = new();
                obrazek.Data = stream.ToArray();
                obrazek.Popis = "Profilový obrázek";
                if (user.IdObrazek == 0 || user.IdObrazek == null)
                {
                    user.IdObrazekNavigation = obrazek;
                    await _context.Obrazky.AddAsync(user.IdObrazekNavigation);
                }
                else 
                {
                    user.IdObrazekNavigation = new() {IdObrazek = (int)user.IdObrazek, Data = obrazek.Data, Popis = obrazek.Popis };
                    _context.Obrazky.Update(user.IdObrazekNavigation);
                }
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                ModelContext.User = user;
            }

            if (user.Role == Role.REGISTERED)
                return View("MyAccountCustomer", new UserCustomerViewModel(user, _context));
            else
                return View("MyAccountEmployee", new UserEmployeeViewModel(user, _context));

        }

        public async Task<IActionResult> RetrieveImage(int id)
        {
            var obrazek = await _context.Obrazky.FindAsync(id);
            byte[] data = obrazek.Data;
            return File(data, "image/jpg");
        }

        private bool UserWithEmailExists(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }

        private bool UserPasswordCorrect(string email, string password)
        {
            return _context.Users.Where(e => e.Password == password).Any(e => e.Email == email);
        }

        //metoda prevede zadane uzivatelem heslo do hashovane podoby
        private string MD5HashedPassword(string password)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash)
           .Replace("-", string.Empty)
           .ToLower();
            return encoded;
        }
    }

}
