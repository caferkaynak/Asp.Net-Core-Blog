using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using caferkaynakblog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace caferkaynakblog.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> singinManager;
        private IPasswordHasher<User> passwordHasher;
        private IPasswordValidator<User> passwordValidator;
        public AccountController(UserManager<User> _userManager, SignInManager<User> _singinManager, IPasswordHasher<User> _passwordHasher, IPasswordValidator<User> _passwordValidator)
        {
            userManager = _userManager;
            singinManager = _singinManager;
            passwordHasher = _passwordHasher;
            passwordValidator = _passwordValidator;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUser model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    await singinManager.SignOutAsync();
                    var result1 = await singinManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result1.Succeeded)
                    {
                        TempData["password"] = model.Password;
                        return RedirectToAction("Index", "Panel");
                    }
                }
                ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalıdır");
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.UserName = model.Username;
                user.Email = model.Email;
                user.PasswordHash = passwordHasher.HashPassword(user, model.Password);
                var control = await userManager.FindByEmailAsync(model.Email);
                var validpass = await passwordValidator.ValidateAsync(userManager, user, model.Password);
                if (validpass.Succeeded)
                {
                    if (control == null)
                    {
                        if (model.Password == model.PasswordT)
                        {
                            var result = await userManager.CreateAsync(user);
                            if (result.Succeeded)
                                return RedirectToAction("Login", "Account");
                        }
                        else
                            ModelState.AddModelError("Hata", "Şifreyi iki kere aynı şekilde giriniz");
                    }
                    else
                        ModelState.AddModelError("Hata", "Aynı Mail Adresi Kullanılmaktadır.");
                }
                else
                    ModelState.AddModelError("Hata", "Lütfen şifreyi 'Rakam','Harf (Büyük ve Küçük)' ve özel karakter içerecek şekilde giriniz");
            }
            return View();
        }
        public IActionResult Logout()
        {
            singinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
