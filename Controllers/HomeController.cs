using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using Microsoft.AspNetCore.Http;

namespace Vtc_Freelancer.Controllers
{
    public class HomeController : Controller
    {
        private UserService userService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserService userService)
        {
            this.userService = userService;
            _logger = logger;
        }

        public IActionResult Index()
        {

            // var UserId = HttpContext.Session.GetInt32("UserId");
            // ViewBag.UserId = UserId;

            // var UserName = HttpContext.Session.GetString("UserName");
            // ViewBag.UserName = UserName;
            // Console.WriteLine(UserName);
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                int? userId = HttpContext.Session.GetInt32("UserId");
                // Console.WriteLine(userId);
                Users userads = userService.GetUsersByID(userId);
                ViewBag.UserName = userads.UserName;
                return View();
            }
            return Redirect("/Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
        public IActionResult EditProfile()
        {
            // HttpContext.Session.SetInt32("UserId", user.UserId);
            // HttpContext.Session.SetString("UserName", user.UserName);
            int? userId = HttpContext.Session.GetInt32("UserId");
            // Console.WriteLine(userId);
            Users userads = userService.GetUsersByID(userId);
            ViewBag.UserName = userads.UserName;
            // var UserId = HttpContext.Session.GetString("UserName");
            // ViewBag.UserId = UserId;
            // var UserName = HttpContext.Session.GetString("UserName");
            // ViewBag.UserName = UserName;
            return View();
        }


    }
}
