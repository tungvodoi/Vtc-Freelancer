
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
        private readonly ILogger<HomeController> _logger;

        public SellerController(ILogger<HomeController> logger, UserService userService, GigService gigService)
        {
            this.userService = userService;
            this.gigService = gigService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }
        // [HttpGet("/fdasfaf")]
        [Route("{username}")]
        [HttpGet]
        public IActionResult ProfileSeller(string username)
        {
            if (username == null)
            {
                return Redirect("/");
            }
            Users users = userService.GetUserByUsername(username);
            if (users == null)
            {
                return Redirect("/");
            }

            Seller seller = userService.GetSellerByUserID(users.UserId);
            List<Service> services = new List<Service>();
            services = gigService.GetServicesBySellerId(seller.SellerId);
            if (seller == null)
            {
                return Redirect("/");
            }
            ViewBag.userProfile = users;
            ViewBag.userAvatar = users.Avatar;
            ViewBag.listServiceProfile = services;
            return View();
        }
    }
}