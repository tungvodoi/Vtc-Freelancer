using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using Vtc_Freelancer.ActionFilter;
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
                int? Qty = HttpContext.Session.GetInt32("Quantity");
                if (Qty != null)
                {
                    ViewBag.Quantity = Qty;
                }
                else
                {
                    ViewBag.Quantity = 1;
                }
                return View(package);
            }
            else
            {
                return Redirect("/");
            }
        }
        [Authentication]
        [HttpGet("/Order/Payment")]
        public IActionResult Payment(int PackageId, int Quantity)
        {
            Package package = orderService.GetPackageByPackageId(PackageId);
            HttpContext.Session.SetInt32("Quantity", Quantity);
            if (package != null)
            {
                package.Service = orderService.GetServiceByServiceId(package.ServiceId);
                package.Service.ListImage = adminService.GetListImageService(package.ServiceId);
                int? Qty = HttpContext.Session.GetInt32("Quantity");
                if (Qty != null)
                {
                    ViewBag.Quantity = Qty;
                }
                return View(package);
            }
            else
            {
                return Redirect("/");
            }
        }
        // [Authentication]
        [HttpGet("/Order/Requirement")]
        public IActionResult Requirement(int PackageId, int Quantity)
        {
            Package package = orderService.GetPackageByPackageId(PackageId);
            HttpContext.Session.SetInt32("Quantity", Quantity);
            if (package != null)
            {
                package.Service = orderService.GetServiceByServiceId(package.ServiceId);
                package.Service.ListImage = adminService.GetListImageService(package.ServiceId);
                int? Qty = HttpContext.Session.GetInt32("Quantity");
                if (Qty != null)
                {
                    ViewBag.Quantity = Qty;
                }
                return View(package);
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}