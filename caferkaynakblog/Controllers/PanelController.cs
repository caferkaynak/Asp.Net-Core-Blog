﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using caferkaynakblog.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace caferkaynakblog.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        private IRepository repo;
        private UserManager<User> userManager;
        private IPasswordValidator<User> passwordValidator;
        private IPasswordHasher<User> passwordHasher;
        
        public PanelController(IRepository rep, UserManager<User> _userManager, IPasswordValidator<User> _passwordValidator, IPasswordHasher<User> _passwordHasher)
        {
            repo = rep;
            userManager = _userManager;
            passwordHasher = _passwordHasher;
            passwordValidator = _passwordValidator;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Adminoptions()
        {
            var UpdateUser = new UpdateUser();
            return View(UpdateUser);
        }
        [HttpPost]
        public async Task<IActionResult> Adminoptions(UpdateUser model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                user.Email = model.Email;
                var pass = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.LastPassword);
                if (pass != 0)
                {
                    if (model.Password == model.PasswordT)
                    {
                        var validpass = await passwordValidator.ValidateAsync(userManager, user, model.Password);
                        if (validpass.Succeeded)
                        {
                            user.PasswordHash = passwordHasher.HashPassword(user, model.Password);
                            ModelState.AddModelError("Succeeded", "Succeeded");
                        }
                        else
                            ModelState.AddModelError("Hata", "Yeni şifreyi kurallara uygun giriniz");
                    }
                    else
                        ModelState.AddModelError("Hata", "Lütfen yeni şifreyi 2 kere doğru giriniz");
                    await userManager.UpdateAsync(user);
                }
                else
                    ModelState.AddModelError("Hata", "Last password error");
            }
            return View();
        }
        public IActionResult Category()
        {
            CategoryViewModel categories = new CategoryViewModel();
            categories.Categories = repo.Categories.ToList();
            return View(categories);
        }
        [HttpPost]
        public IActionResult CategorySave(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var categoryvalid = repo.Categories.Any(w => w.CategoryName == model.category.CategoryName);
                if (!categoryvalid)
                    repo.CreateCategory(model.category);
                else
                    ModelState.AddModelError("Error", "Aynı kategori adı bulunmaktadır.");
            }
            return RedirectToAction("Category", "Panel");
        }
        public IActionResult CategoryEdit()
        {
            CategoryViewModel categories = new CategoryViewModel();
            categories.Categories = repo.Categories.ToList();
            return View(categories);
        }
        public IActionResult CategoryUpdate(CategoryViewModel model)
        {

            if (ModelState.IsValid)
            {
                var category = repo.Categories.Where(w => w.Id == model.category.Id).FirstOrDefault();
                category.CategoryName = model.category.CategoryName;
                repo.UpdateCategory(category);
            }
            return RedirectToAction("CategoryEdit", "Panel");
        }
        public IActionResult CategoryDelete(CategoryViewModel model)
        {

            if (ModelState.IsValid)
            {
                var category = repo.Categories.Where(w => w.Id == model.category.Id).FirstOrDefault();
                repo.DeleteCategory(category);
            }
            return RedirectToAction("CategoryEdit", "Panel");
        }
        public IActionResult CreateEntry()
        {
            EntryViewModel entry = new EntryViewModel();
            entry.categories = repo.Categories.ToList();
            entry.tags = repo.Tags.ToList();
            return View(entry);
        }
        [HttpPost]
        public IActionResult CreateEntry(EntryViewModel model)
        {
            if (ModelState.IsValid)
            {
            var user = repo.Users.FirstOrDefault(w => w.UserName == User.Identity.Name);
                model.entry.UsersId = user.Id;
                repo.CreateEntry(model.entry);
                return RedirectToAction("EntryList", "Panel");
            }
            return View();
        }
        public IActionResult Entry()
        {
            return View();
        }
    }
}