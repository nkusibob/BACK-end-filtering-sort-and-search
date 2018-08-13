using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DomainPsr03951.Models;
using WebApplication1.Model;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly psr03951DataBaseContext db;
        public HomeController(psr03951DataBaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var UserPerGroup =
                    from user in db.User
                    group user by user.IdGroupNavigation.Name into Group
                    select new
                    {
                        Group = Group.Key,
                        Count = Group.Count(),
                    };
            var countAllUsers = db.User.Count();
            ViewBag.countUsers = countAllUsers;
            var users = db.User;
            Dictionary<string,int> mylist = new Dictionary<string,int>();
            var groups = UserPerGroup.ToList();
            foreach (var item in groups)
            {
                mylist.Add(item.Group,item.Count) ;
            }
            ViewBag.users = users;
            ViewBag.mylist = mylist;
           
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
