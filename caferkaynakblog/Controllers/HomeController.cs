using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        
    }
}