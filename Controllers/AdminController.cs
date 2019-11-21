using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using Vtc_Freelancer.ActionFilter;

namespace Vtc_Freelancer.Controllers
{
    // [Authentication]
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

        [HttpGet("/Admin/Dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet("/Admin/Services")]

        public IActionResult Services(string Search)
        {
            ViewBag.ListServices = adminService.GetListServices(Search);
            return View();
        }
        [HttpGet("/Admin/Error")]

        public IActionResult Error()
        {
            return View();
        }
        [HttpGet("/Admin/ManagerReports")]

        public IActionResult ManagerReports(string Search)
        {
            ViewBag.ListReport = adminService.GetListReport(Search);
            return View();
        }
        [HttpGet("/Admin/ChangeStatusReport")]

        public IActionResult ChangeStatusReport(int ReportId)
        {
            try
            {
                adminService.ChangeStatusReport(ReportId);
                return Redirect("/Admin/ManagerReports");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return Redirect("/Admin/ManagerUsers");
                throw;
            }

        }
        [HttpGet("/Admin/ManagerUsers")]
        public IActionResult ManagerUsers(string Search)
        {
            ViewBag.ListUsers = adminService.GetListUsers(Search);
            return View();
        }

        [HttpGet("/Admin/ChangeStatusUser")]

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
        [HttpGet("/Admin/Charts")]
        public IActionResult Charts()
        {
            return View();
        }
        [HttpGet("/Admin/Tables")]

        public IActionResult Tables()
        {
            return View();
        }
        [HttpGet("/Admin/UtilitiesAnimation")]

        public IActionResult UtilitiesAnimation()
        {
            return View();
        }
        [HttpGet("/Admin/UtilitiesBorder")]

        public IActionResult UtilitiesBorder()
        {
            return View();
        }
        [HttpGet("/Admin/UtilitiesColor")]

        public IActionResult UtilitiesColor()
        {
            return View();
        }
        [HttpGet("/Admin/UtilitiesOther")]

        public IActionResult UtilitiesOther()
        {
            return View();
        }
    }
}