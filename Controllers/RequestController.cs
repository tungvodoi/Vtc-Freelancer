using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Vtc_Freelancer.ActionFilter;

namespace Vtc_Freelancer.Controllers
{
    public class RequestController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private MyDbContext dbContext;
        private RequestService requestService;
        private AdminService adminService;
        private UserService userService;
        private OrderService orderService;
        private GigService gigService;
        public RequestController(MyDbContext dbContext, RequestService requestService, AdminService adminService, UserService userService, OrderService orderService, IHostingEnvironment IHostingEnvironment, GigService gigService)
        {
            this.userService = userService;
            this.dbContext = dbContext;
            this.requestService = requestService;
            this.adminService = adminService;
            this._environment = IHostingEnvironment;
            this.orderService = orderService;
            this.gigService = gigService;
        }
        [Authentication]
        [HttpGet("/create_request")]
        public IActionResult FormInputRequest()
        {
            Users users = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));
            ViewBag.UserName = users.UserName;
            ViewBag.userAvatar = users.Avatar;
            ViewBag.IsSeller = users.IsSeller;
            ViewBag.ListOrder = orderService.GetListOrderbyUserId(users.UserId);
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

            return View("Request");
        }

        [HttpPost("/create_request")]
        public IActionResult CreateRequest(string inputRequest, string category, string SubCategory, string inputDeliveredTime, double inputBudget, string name)
        {
            string urlFile = "";
            var UserId = HttpContext.Session.GetInt32("UserId");
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
                        fileName = Path.Combine(_environment.WebRootPath, "FileRequest/") + $@"\{newFileName}";

                        // if you want to store path of folder in database
                        urlFile = "FileRequest/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
            }
            if (requestService.CreateRequest(UserId, inputRequest, category, SubCategory, inputDeliveredTime, inputBudget, urlFile))
            {
                return Redirect("/");
            }
            else
            {
                return Redirect("/manager_request");
            }
        }

        [HttpGet("/manager_request")]
        public IActionResult ViewRequests()
        {
            Users users = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));
            if (users != null)
            {
                ViewBag.UserName = users.UserName;
                ViewBag.userAvatar = users.Avatar;
                ViewBag.IsSeller = users.IsSeller;
                List<Request> listRequest = requestService.getListRequestByUserId(users.UserId);
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
                return View(listRequest);
            }
            return Redirect("/");
        }
        [HttpGet("/Request")]
        public IActionResult ListRequest()
        {
            Users users = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));

            if (users != null)
            {
                Seller seller = userService.GetSellerByUserID(users.UserId);
                ViewBag.UserName = users.UserName;
                ViewBag.userAvatar = users.Avatar;
                ViewBag.IsSeller = users.IsSeller;
                List<Category> category = userService.getCategoryOfSellerByUserId(users.UserId);
                List<Request> listRequest = requestService.getListRequestByCategoryOfSeller(category);
                List<Category> listcategory = new List<Category>();
                List<Service> services = new List<Service>();
                services = gigService.GetServicesBySellerId(seller.SellerId);
                foreach (var item in services)
                {
                    item.ListImage = adminService.GetListImageService(item.ServiceId);
                }
                ViewBag.myService = services;
                listcategory = adminService.GetListCategoryBy();
                foreach (var item in listcategory)
                {
                    item.subsCategory = adminService.GetListSubCategoryByParentId(item.CategoryId);
                }
                if (listcategory != null)
                {
                    ViewBag.listcategory = listcategory;
                }
                return View(listRequest);
            }
            else
            {
                return Redirect("/");
            }
        }





        [HttpGet("/manager_your_request")]
        public IActionResult ManagerRequest()
        {
            Users users = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));

            if (users != null)
            {
                // Seller seller = userService.GetSellerByUserID(users.UserId);
                // ViewBag.UserName = users.UserName;
                // ViewBag.userAvatar = users.Avatar;
                // ViewBag.IsSeller = users.IsSeller;
                // List<Category> category = userService.getCategoryOfSellerByUserId(users.UserId);
                List<Category> listcategory = new List<Category>();

                List<Request> listRequest = requestService.GetRequestByUserId(users.UserId);


                listcategory = adminService.GetListCategoryBy();
                foreach (var item in listcategory)
                {
                    item.subsCategory = adminService.GetListSubCategoryByParentId(item.CategoryId);
                }
                if (listcategory != null)
                {
                    ViewBag.listcategory = listcategory;
                }
                return View(listRequest);
            }
            else
            {
                return Redirect("/");
            }
        }








        [HttpPost("/sendOffer")]
        public IActionResult SendOffer(int RequestId, int ServiceId, string description)
        {
            Seller seller = userService.GetSellerByUserID(HttpContext.Session.GetInt32("UserId"));
            if (seller != null)
            {
                if (orderService.SendOffer(seller, RequestId, ServiceId, description))
                {
                    return Redirect("/Request");
                }
            }
            return Redirect("/");
        }

        [HttpGet("/Offer")]
        public IActionResult ListOffer(int? requestId)
        {
            Users users = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));

            if (users != null)
            {
                Seller seller = userService.GetSellerByUserID(users.UserId);
                ViewBag.UserName = users.UserName;
                ViewBag.userAvatar = users.Avatar;
                ViewBag.IsSeller = users.IsSeller;
                List<Category> category = userService.getCategoryOfSellerByUserId(users.UserId);
                List<Category> listcategory = new List<Category>();
                List<Offer> offers = new List<Offer>();
                offers = requestService.GetOffersByRequestId(requestId);
                foreach (var item in offers)
                {
                    item.Service = new Service();
                    item.Service = gigService.GetServiceByID(item.ServiceId);
                    System.Console.WriteLine(item.Service.Title + "ppp");
                }
                // List<Service> services = new List<Service>();
                // services = gigService.GetServiceByID();
                // foreach (var item in services)
                // {
                //     item.ListImage = adminService.GetListImageService(item.ServiceId);
                // }
                // ViewBag.myService = services;
                listcategory = adminService.GetListCategoryBy();
                foreach (var item in listcategory)
                {
                    item.subsCategory = adminService.GetListSubCategoryByParentId(item.CategoryId);
                }
                if (listcategory != null)
                {
                    ViewBag.listcategory = listcategory;
                }
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }


    }
}