﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using Microsoft.AspNetCore.Http;
using Vtc_Freelancer.ActionFilter;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Vtc_Freelancer.Controllers
{
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
        [HttpGet("/HomePage")]
        public IActionResult HomePage()
        {
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();
            foreach (var item in listcategory)
            {
                item.subsCategory = adminService.GetListSubCategoryByParentId(item.CategoryId);
            }
            List<Service> services = new List<Service>();
            services = adminService.GetListServices("");
            if (listcategory != null)
            {
                ViewBag.listcategory = listcategory;
            }

            return View();
        }
        [Authentication]
        public IActionResult Index()
        {
            HttpContext.Session.Remove("Quantity");
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();
            foreach (var item in listcategory)
            {
                item.subsCategory = adminService.GetListSubCategoryByParentId(item.CategoryId);
            }
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

            }
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View(services);
        }
        public IActionResult EditProfile()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            Users userads = userService.GetUsersByID(userId);
            ViewBag.UserName = userads.UserName;
            return View();
        }
        public IActionResult Search(string search_content)
        {
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();
            foreach (var item in listcategory)
            {
                item.subsCategory = adminService.GetListSubCategoryByParentId(item.CategoryId);
            }
            if (listcategory != null)
            {
                ViewBag.listcategory = listcategory;
            }
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            if (search_content == null)
            {
                return Redirect("/");
            }
            List<Service> services = new List<Service>();
            services = adminService.GetListServices(search_content);
            if (services == null)
            {
                return Redirect("/");
            }
            foreach (var item in services)
            {
                item.ListImage = adminService.GetListImageService(item.ServiceId);
            }
            ViewBag.ListServicesSearch = services;
            ViewBag.searchResult = search_content;

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
