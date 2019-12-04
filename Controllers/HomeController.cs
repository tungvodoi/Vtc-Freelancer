﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using Microsoft.AspNetCore.Http;
using Vtc_Freelancer.ActionFilter;
using System;


namespace Vtc_Freelancer.Controllers
{
    // [Authentication]
    public class HomeController : Controller
    {
        private UserService userService;
        private AdminService adminService;
        private readonly ILogger<HomeController> _logger;
        public HomeController(UserService userService, AdminService adminService, ILogger<HomeController> logger)
        {
            this._logger = logger;
            this.userService = userService;
            this.adminService = adminService;
        }
        public IActionResult Index()
        {
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();
            List<Service> services = new List<Service>();
            services = adminService.GetListServices("");
            foreach (var item in services)
            {
                item.ListImage = adminService.GetListImageService(item.ServiceId);
            }
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                int? userId = HttpContext.Session.GetInt32("UserId");
                Users userads = userService.GetUsersByID(userId);
                ViewBag.UserName = userads.UserName;
                ViewBag.userAvatar = userads.Avatar;
                //Lay Session lan 2
                ViewBag.IsSeller = HttpContext.Session.GetInt32("IsSeller");
                // HttpContext.Session.Remove("IsSeller");
                ViewBag.SellerId = HttpContext.Session.GetInt32("SellerId");
            }

            if (listcategory != null)
            {
                ViewBag.listcategory = listcategory;
                return View(services);
            }
            return View(services);
        }
        public IActionResult EditProfile()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            Users userads = userService.GetUsersByID(userId);
            ViewBag.UserName = userads.UserName;
            return View();
        }
        [HttpGet("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
