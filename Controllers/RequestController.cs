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
    [Authentication]
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

            Users user = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));
            if (user != null)
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
                            fileName = Path.Combine(_environment.WebRootPath, $@"FileRequest/{user.UserName}") + $@"\{newFileName}";

                            // if you want to store path of folder in database
                            urlFile = $@"FileRequest/{user.UserName}/" + newFileName;

                            if (Directory.Exists($@"FileRequest/{user.UserName}"))
                            {
                                using (FileStream fs = System.IO.File.Create(fileName))
                                {
                                    file.CopyTo(fs);
                                    fs.Flush();
                                }
                            }
                            else
                            {
                                DirectoryInfo di = Directory.CreateDirectory($@"wwwroot/FileRequest/{user.UserName}");
                                using (FileStream fs = System.IO.File.Create(fileName))
                                {
                                    file.CopyTo(fs);
                                    fs.Flush();
                                }
                            }
                        }
                    }
                }
                if (requestService.CreateRequest(user.UserId, inputRequest, category, SubCategory, inputDeliveredTime, inputBudget, urlFile))
                {
                    return Redirect("/");
                }
            }
            return Redirect("/manager_request");
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
                List<Request> listRequest = requestService.getListRequestByCategory(category);
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

        [HttpGet("/manager_request")]
        public IActionResult ManagerRequest()
        {
            Users users = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));

            if (users != null)
            {
                ViewBag.UserName = users.UserName;
                ViewBag.userAvatar = users.Avatar;
                ViewBag.ListOrder = orderService.GetListOrderbyUserId(users.UserId);

                ViewBag.IsSeller = HttpContext.Session.GetInt32("IsSeller");
                // HttpContext.Session.Remove("IsSeller");
                ViewBag.SellerId = HttpContext.Session.GetInt32("SellerId");
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
                // Seller seller = userService.GetSellerByUserID(users.UserId);
                ViewBag.UserName = users.UserName;
                ViewBag.userAvatar = users.Avatar;
                ViewBag.IsSeller = users.IsSeller;
                // List<Category> category = userService.getCategoryOfSellerByUserId(users.UserId);
                List<Category> listcategory = new List<Category>();
                List<Offer> offers = new List<Offer>();
                offers = requestService.GetOffersByRequestId(requestId);
                foreach (var item in offers)
                {
                    item.Service = new Service();
                    item.Service = gigService.GetServiceByID(item.ServiceId);
                    item.users = new Users();
                    item.Service.ListImage = new List<ImageService>();
                    item.users = userService.GetUserByUserId(item.SellerId);
                    item.Service.ListImage = gigService.GetListImagesByServiceId(item.ServiceId);
                }
                ViewBag.listOffers = offers;
                Request request = new Request();
                ViewBag.ListOrder = orderService.GetListOrderbyUserId(users.UserId);
                request = requestService.getRequestByRequestId(requestId);
                ViewBag.request = request;
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