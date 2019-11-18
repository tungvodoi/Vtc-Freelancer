﻿using System;
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
<<<<<<< HEAD
        // private MyDbContext dbContext;
        private UserService userService;
        private static Users user;
        private readonly ILogger<HomeController> _logger;
        private List<Service> _listServices = new List<Service>
        {
          new Service { ServiceId = 1, Title ="I will develop android app or iphone app"},
          new Service { ServiceId = 2, Title ="abc"}
        };
        public HomeController(ILogger<HomeController> logger, UserService userService)
        {
            this.userService = userService;
            _logger = logger;
        }
        // public IActionResult Login(string email, string password)
        // {
        //   // if (test1.Username == "admin" && test1.Username1 == "admin")
        //   // {
        //   //   test1.Text = "Welcome Linh";
        //   // }
        //   // else
        //   // {
        //   //   test1.Text = "Mr.Hoang said : 'Djt nhau'";
        //   // }
        //   // return View(test1);
        //   HttpContext.Session.SetInt32("UserId", user.UserId);

        //   user = userService.Login(email, password);
        //   // HttpContext.Session.SetString("UserName", user.UserName);

        //   Console.WriteLine("1");
        //   Console.WriteLine(user.UserName);
=======
        private UserService userService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserService userService)
        {
            this.userService = userService;
            _logger = logger;
        }
>>>>>>> ac0681050abed167eed9caa37f2357baf18a8f16

        public IActionResult Index()
        {

<<<<<<< HEAD
        //   return View();
        // }
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

                ViewBag.ListServices = _listServices;

                return View();
            }
            foreach (var item in _listServices)
            {
                Console.WriteLine(item.ServiceId);
            }
            // return Redirect("/Login");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
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


=======
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


>>>>>>> ac0681050abed167eed9caa37f2357baf18a8f16
    }
}
