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
        private ChatService chatService;
        public OrderController(UserService userService, OrderService orderService, AdminService adminService, ChatService chatService, IHostingEnvironment _environment)
        {
            this._environment = _environment;
            this.userService = userService;
            this.orderService = orderService;
            this.adminService = adminService;
            this.chatService = chatService;
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
                    // package.Service = orderService.GetServiceByServiceId(package.ServiceId);
                    // package.Service.ListImage = adminService.GetListImageService(package.ServiceId);
                    // int? Qty = HttpContext.Session.GetInt32("Quantity");
                    // if (Qty != null)
                    // {
                    //     ViewBag.Quantity = Qty;
                    // }
                    // else
                    // {
                    //     ViewBag.Quantity = 1;
                    // }
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
                int? UserId = HttpContext.Session.GetInt32("UserId");
                if (UserId == order.UserId)
                {
                    order.Service = orderService.GetServiceByServiceId(order.ServiceId);
                    order.Package = orderService.GetPackageByPackageId(order.PackageId);
                    order.Service.ListImage = adminService.GetListImageService(order.ServiceId);
                    return View(order);
                }
            }
            return Redirect("/");
        }

        [HttpPost("/Order/StartOrder")]
        public IActionResult StartOrder(int OrderId, string ContentRequire, string name)
        {
            Users users = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));
            List<Attachments> listFile = new List<Attachments>();
            if (users != null)
            {
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
                            fileName = Path.Combine(_environment.WebRootPath, "Attachments/") + $@"\{newFileName}";

                            // if you want to store path of folder in database
                            PathDB = "Attachments/" + newFileName;

                            Attachments fileProduct = new Attachments();
                            fileProduct.LinkFile = PathDB;
                            fileProduct.FileName = newFileName;
                            listFile.Add(fileProduct);

                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }
                if (orderService.StartOrder(users.UserId, OrderId, ContentRequire, listFile))
                {
                    return Redirect("/order?orderId=" + OrderId);
                }
            }
            return Redirect("/");
        }

        [HttpGet("/order")]
        public IActionResult StatusOrder(int OrderId)
        {
            Orders order = orderService.GetOrderByOrderId(OrderId);
            if (order != null)
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
                int? UserId = HttpContext.Session.GetInt32("UserId");
                Users userads = userService.GetUsersByID(UserId);
                if (userads != null)
                {
                    order.Service = orderService.GetServiceByServiceId(order.ServiceId);
                    order.Package = orderService.GetPackageByPackageId(order.PackageId);
                    order.Users = userService.GetUserByUserId(order.UserId);
                    order.Service.ListImage = adminService.GetListImageService(order.ServiceId);
                    Service se = orderService.GetServiceByServiceId(order.ServiceId);
                    order.Service.Seller = orderService.GetSellerBySellerId(se.SellerId);
                    List<Conversation> ListConversation = chatService.GetListConversationByUserId(order.Service.SellerId, order.UserId);

                    if (UserId == order.UserId || UserId == order.Service.Seller.User.UserId)
                    {
                        ViewBag.Chat = ListConversation;
                        ViewBag.ListOrder = orderService.GetListOrderbyUserId(UserId);
                        ViewBag.UserName = userads.UserName;
                        ViewBag.UserId = userads.UserId;
                        ViewBag.userAvatar = userads.Avatar;
                        ViewBag.IsSeller = userads.IsSeller;
                        ViewBag.Delivery = order.OrderStartTime.AddDays(order.Package.DeliveryTime);
                        return View(order);
                    }
                }
            }
            return Redirect("/");
        }

        [HttpPost("/DeleverWork")]
        public IActionResult DeleverWork(int OrderId, string contentResult, string name)
        {
            Users users = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));
            if (users != null)
            {
                List<Attachments> listFile = new List<Attachments>();
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
                            fileName = Path.Combine(_environment.WebRootPath, "Attachments/") + $@"\{newFileName}";

                            // if you want to store path of folder in database
                            PathDB = "Attachments/" + newFileName;

                            Attachments fileProduct = new Attachments();
                            fileProduct.LinkFile = PathDB;
                            fileProduct.FileName = newFileName;
                            listFile.Add(fileProduct);

                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                    if (orderService.DeliverWord(users.UserId, OrderId, contentResult, listFile))
                    {
                        return Redirect("/order?orderId=" + OrderId);
                    }
                }
            }
            return Redirect("/");
        }
        [HttpPost("/AddNote")]
        public IActionResult AddNote(int OrderId, string noteContent)
        {
            Users users = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));
            if (users != null)
            {
                if (orderService.Addnote(OrderId, noteContent))
                {
                    return Redirect("/order?orderId=" + OrderId);
                }
            }
            return Redirect("/");
        }
        [HttpPost("/CancelOrder")]
        public IActionResult CancelOrder(int OrderId, string contentCancelOrder)
        {
            Users users = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));
            if (users != null)
            {
                if (orderService.CancelOrder(OrderId, contentCancelOrder))
                {
                    return Redirect("/manager_orders");
                }
            }
            return Redirect("/");
        }

        [HttpGet("/manager_orders")]
        public IActionResult ViewOrders()
        {
            Users users = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));
            if (users != null)
            {
                ViewBag.UserName = users.UserName;
                ViewBag.userAvatar = users.Avatar;
                ViewBag.IsSeller = users.IsSeller;
                List<Orders> listOrders = orderService.GetListOrderbyUserId(users.UserId);
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
                return View(listOrders);
            }
            return Redirect("/");
        }
    }
}