using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using System.Linq;
using System.Collections.Generic;

namespace Vtc_Freelancer.Controllers
{
    public class OrderController : Controller
    {
        private UserService userService;
        private OrderService orderService;
        private AdminService adminService;
        public OrderController(UserService userService, OrderService orderService, AdminService adminService)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.adminService = adminService;
        }
        [HttpGet("/Customize/Order")]
        public IActionResult Order(int PackageId)
        {
            Package package = orderService.GetPackageByPackageId(PackageId);
            if (package != null)
            {
                package.Service = orderService.GetServiceByServiceId(package.ServiceId);
                package.Service.ListImage = adminService.GetListImageService(package.ServiceId);
                return View(package);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpGet("/Order/Payment")]
        public IActionResult Payment(int PackageId)
        {
            Package package = orderService.GetPackageByPackageId(PackageId);
            if (package != null)
            {
                package.Service = orderService.GetServiceByServiceId(package.ServiceId);
                package.Service.ListImage = adminService.GetListImageService(package.ServiceId);
                return View(package);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpGet("/Order/Requirement")]
        public IActionResult Requirement()
        {
            Package package = orderService.GetPackageByPackageId(1);
            if (package != null)
            {
                package.Service = orderService.GetServiceByServiceId(package.ServiceId);
                return View(package);
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}