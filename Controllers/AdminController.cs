using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;

namespace Vtc_Freelancer.Controllers
{
    public class AdminController : Controller
    {
        private UserService userService;
        private GigService gigService;
        private AdminService adminService;
        public AdminController(UserService userService, GigService gigService, AdminService adminService)
        {
            this.userService = userService;
            this.gigService = gigService;
            this.adminService = adminService;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult ActiveService()
        {
            ViewBag.ListServicesHadActive = adminService.GetListServicesHadActive();
            return View();
        }

        public IActionResult InactiveService()
        {
            ViewBag.ListServicesInactive = adminService.GetListServicesInactive();
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Blank()
        {
            return View();
        }
        public IActionResult ManagerUsers(string Username)
        {
            ViewBag.ListUsers = adminService.GetListUsers(Username);
            return View();
        }

        [HttpGet]
        public IActionResult ChangeStatusUser(int UserId)
        {
            try
            {
                adminService.ChangeStatusUser(UserId);
                return Redirect("/Admin/ManagerUsers");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return Redirect("/Admin/ManagerUsers");
                throw;
            }

        }
        public IActionResult Charts()
        {
            return View();
        }

        public IActionResult Tables()
        {
            return View();
        }

        public IActionResult UtilitiesAnimation()
        {
            return View();
        }

        public IActionResult UtilitiesBorder()
        {
            return View();
        }

        public IActionResult UtilitiesColor()
        {
            return View();
        }

        public IActionResult UtilitiesOther()
        {
            return View();
        }
    }
}