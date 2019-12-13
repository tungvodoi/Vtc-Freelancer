
using System.Collections.Generic;
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
    public class SellerController : Controller
    {
        private UserService userService;
        private GigService gigService;
        private AdminService adminService;
        private readonly ILogger<HomeController> _logger;

        public SellerController(ILogger<HomeController> logger, UserService userService, GigService gigService, AdminService adminService)
        {
            this.userService = userService;
            this.adminService = adminService;
            this.gigService = gigService;
            _logger = logger;
        }
        [Authentication]

        public IActionResult Index()
        {
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();
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
            int? userId = HttpContext.Session.GetInt32("UserId");
            Users userads = userService.GetUsersByID(userId);
            ViewBag.userAvatar = userads.Avatar;
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }
        // [HttpGet("/fdasfaf")]
        [Route("{username}")]
        [HttpGet]
        public IActionResult ProfileSeller(string username)
        {

            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();
            if (listcategory != null)
            {
                ViewBag.listcategory = listcategory;
            }
            if (username == null)
            {
                return Redirect("/");
            }
            Users users = userService.GetUserByUsername(username);
            if (users == null)
            {
                return Redirect("/");
            }
            ViewBag.UserLoged = HttpContext.Session.GetString("UserName");
            Seller seller = userService.GetSellerByUserID(users.UserId);
            List<Service> services = new List<Service>();
            services = gigService.GetServicesBySellerId(seller.SellerId);
            if (seller == null)
            {
                return Redirect("/");
            }
            foreach (var item in services)
            {
                item.ListImage = adminService.GetListImageService(item.ServiceId);
            }
            ViewBag.sellerprofile = seller;
            ViewBag.userProfile = users;
            ViewBag.userAvatar = users.Avatar;
            ViewBag.listServiceProfile = services;

            return View(services);
        }
        [HttpPost]
        public bool UpdateDescription(string description)
        {
            Seller seller = new Seller();
            seller.Description = description;
            int? UserId = HttpContext.Session.GetInt32("UserId");
            bool check = userService.UpdateDescription(UserId, description);
            if (check)
            {
                return true;
            }
            return false;
        }
    }
}