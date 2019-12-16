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
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Vtc_Freelancer.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private UserService userService;
        private OrderService orderService;
        private AdminService adminService;
        public OrderController(UserService userService, OrderService orderService, AdminService adminService, IHostingEnvironment _environment)
        {
            this._environment = _environment;
            this.userService = userService;
            this.orderService = orderService;
            this.adminService = adminService;
        }
        
        [Authentication]
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
        [HttpPost("/Customize/CreateOrder")]
        public IActionResult CreateOrder(int PackageId, int selectQuantity)
        {
            Package package = orderService.GetPackageByPackageId(PackageId);
            int? UserId = HttpContext.Session.GetInt32("UserId");
            Users user = userService.GetUsersByID(UserId);
            if (package != null)
            {
                if (user != null)
                {
                    if (orderService.CreateOrder(user.UserId, package.ServiceId, PackageId, selectQuantity))
                    {
                        return Redirect("/Order/Payment?PackageId=" + PackageId + "&Quantity=" + selectQuantity);
                    }
                }
            }
            return Redirect("/");
        }
        // [Authentication]
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

        [HttpPost("/Order/Payment")]
        public IActionResult Payment(int PackageId, string save)
        {
            Package package = orderService.GetPackageByPackageId(PackageId);
            if (package != null)
            {
                if (save == "on")
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
                }
                Orders order = orderService.GetOrderByPackageId(package.PackageId);
                if (order != null)
                {
                    return Redirect("/Order/Requirement?OrderId=" + order.OrderId);
                }
            }
            return Redirect("/");
        }
        // [Authentication]
        [HttpGet("/Order/Requirement")]
        public IActionResult Requirement(int OrderId)
        {
            Orders order = orderService.GetOrderByOrderId(OrderId);
            if (order != null)
            {
                order.Service = orderService.GetServiceByServiceId(order.ServiceId);
                order.Package = orderService.GetPackageByPackageId(order.PackageId);
                order.Service.ListImage = adminService.GetListImageService(order.ServiceId);
                return View(order);
            }
            return Redirect("/");
        }

        [HttpPost("/Order/StartOrder")]
        public IActionResult StartOrder(int OrderId, string ContentRequire, string name)
        {
            string urlFile = "";
            var newFileName = string.Empty;

            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;
                string PathDB = string.Empty;
                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "FileRequire/") + $@"\{newFileName}";

                        // if you want to store path of folder in database
                        urlFile = "FileRequire/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
            }
            if (orderService.StartOrder(OrderId, ContentRequire, urlFile))
            {
                return View("Requirement");
            }
            return Redirect("/");
        }
    }
}