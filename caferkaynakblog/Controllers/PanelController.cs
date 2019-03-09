using System;
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
using Microsoft.AspNetCore.Http;
using System.IO;

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



        public IActionResult Entry()
        {
            EntryViewModel entryViewModel = new EntryViewModel();
            entryViewModel.entries = repo.Entries.ToList();
            entryViewModel.categories = repo.Categories.ToList();
            List<UsersIdName> List = new List<UsersIdName>();
            foreach (var item in repo.Users)
            {
                List.Add(
                    new UsersIdName() { UserId = item.Id, UserName = item.UserName}
                    );
            }
            entryViewModel.usersIdNames = List.ToList();
                return View(entryViewModel);
        }
        public IActionResult UpdateEntry(int id)
        {
            EntryViewModel model = new EntryViewModel();
            model.entry = repo.Entries.Where(w => w.Id == id).FirstOrDefault();
            model.categories = repo.Categories.ToList();
            model.tags = repo.Tags.ToList();
            return View(model);
        }
        public IActionResult DeleteEntry(int id)
        {
            var entry = repo.Entries.Where(w => w.Id == id).FirstOrDefault();
            repo.DeleteEntry(entry);
            return RedirectToAction("Entry", "Panel");
        }
        public IActionResult CreateEntry()
        {
            EntryViewModel model = new EntryViewModel();
            model.categories = repo.Categories.ToList();
            model.tags = repo.Tags.ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEntry(EntryViewModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && model.entry.CategoryId != 0)
                {
                    
                    var path = Path.Combine("\\Users\\Cafer Kaynak\\Source\\Repos\\caferkaynakblog\\caferkaynakblog\\wwwroot\\img\\", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        model.entry.ImageUrl = file.FileName;
                    }
                    var user = repo.Users.FirstOrDefault(w => w.UserName == User.Identity.Name);
                    model.entry.Date = DateTime.Now;
                    model.entry.UsersId = user.Id;
                    repo.CreateEntry(model.entry);
                    return RedirectToAction("Entry", "Panel");
                }
            }
            model.categories = repo.Categories.ToList();
            model.tags = repo.Tags.ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEntry(EntryViewModel model, IFormFile file, int id)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine("\\Users\\Cafer Kaynak\\Source\\Repos\\caferkaynakblog\\caferkaynakblog\\wwwroot\\img\\", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        model.entry.ImageUrl = file.FileName;
                    } 
                }
                var user = repo.Users.FirstOrDefault(w => w.UserName == User.Identity.Name);
                model.entry.Date = DateTime.Now;
                model.entry.UsersId = user.Id;
                repo.UpdateEntry(model.entry);
                return RedirectToAction("Entry", "Panel");
            }
            model.entry = repo.Entries.Where(w => w.Id == id).FirstOrDefault();
            model.categories = repo.Categories.ToList();
            model.tags = repo.Tags.ToList();
            return View(model);
        }



        public IActionResult Tag()
        {
            TagViewModel tags = new TagViewModel();
            tags.tags = repo.Tags.ToList();
            return View(tags);
        }
        public IActionResult TagEdit()
        {
            TagViewModel tags = new TagViewModel();
            tags.tags = repo.Tags.ToList();
            return View(tags);
        }
        [HttpPost]
        public IActionResult TagCreate(TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tagvalid = repo.Tags.Any(w => w.TagName == model.tag.TagName);
                if (!tagvalid)
                    repo.CreateTag(model.tag);
                else
                    ModelState.AddModelError("Error", "Aynı tag adı bulunmaktadır.");
            }
            return RedirectToAction("Tag", "Panel");
        }
        [HttpPost]
        public IActionResult TagUpdate(TagViewModel model)
        {

            if (ModelState.IsValid)
            {
                var tag = repo.Tags.Where(w => w.Id == model.tag.Id).FirstOrDefault();
                tag.TagName = model.tag.TagName;
                repo.UpdateTag(tag);
            }
            return RedirectToAction("TagEdit", "Panel");
        }
        [HttpPost]
        public IActionResult TagDelete(TagViewModel model)
        {

            if (ModelState.IsValid)
            {
                var tag = repo.Tags.Where(w => w.Id == model.tag.Id).FirstOrDefault();
                repo.DeleteTag(tag);
            }
            return RedirectToAction("TagEdit", "Panel");
        }
    }
}