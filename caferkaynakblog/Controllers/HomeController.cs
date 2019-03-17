using System.Collections.Generic;
using System.Linq;
using caferkaynakblog.Models;
using Microsoft.AspNetCore.Mvc;

namespace caferkaynakblog.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;
        public HomeController(IRepository _repository)
        {
            repository = _repository;
        }
        public IActionResult Index()
        {
            EntryViewModel model = new EntryViewModel();
            foreach (var item1 in repository.Entries)
            {
                item1.Content = item1.Content.Substring(0, 550);
            }
            model.entries = repository.Entries.ToList();
            model.categories = repository.Categories.ToList();
            List<UsersIdName> List = new List<UsersIdName>();
            foreach (var item in repository.Users)
            {
                List.Add(
                    new UsersIdName() { UserId = item.Id, UserName = item.UserName }
                    );
            }
            model.usersIdNames = List.ToList();
            return View(model);
        }
        public IActionResult Entry(int id)
        {
            EntryViewModel model = new EntryViewModel();
            List<UsersIdName> List = new List<UsersIdName>();
            foreach (var item in repository.Users)
            {
                List.Add(
                    new UsersIdName() { UserId = item.Id, UserName = item.UserName }
                    );
            }
            model.usersIdNames = List.ToList();
            model.entry = repository.Entries.Where(w => w.Id == id).FirstOrDefault();
            return View(model);
        }
        public IActionResult CategoryList(int id)
        {
            EntryViewModel model = new EntryViewModel();
            List<UsersIdName> List = new List<UsersIdName>();
            foreach (var item in repository.Users)
            {
                List.Add(
                    new UsersIdName() { UserId = item.Id, UserName = item.UserName }
                    );
            }
            model.usersIdNames = List.ToList();
            model.categories = repository.Categories.ToList();
            model.entries = repository.Entries.Where(w => w.CategoryId == id).ToList();
            return View(model);
        }
        
    }
}