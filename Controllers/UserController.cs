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

namespace Vtc_Freelancer.Controllers
{
    public class UserController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private MyDbContext dbContext;
        private UserService userService;
        private AdminService adminService;
        public UserController(MyDbContext dbContext, UserService userService, AdminService adminService, IHostingEnvironment IHostingEnvironment)
        {
            _environment = IHostingEnvironment;
            this.dbContext = dbContext;
            this.userService = userService;
            this.adminService = adminService;
            dbContext.Database.EnsureCreated();
        }
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            return View();
        }
        [HttpPost("/Register")]
        public IActionResult Register(string username, string email, string password)
        {
            if (userService.Register(username, email, password))
            {
                ViewBag.Noti = "Register Successfully :)";
            }
            return Redirect("/Login");
        }
        [HttpGet("/Register")]
        public IActionResult Register()
        {
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();
            if (listcategory != null)
            {
                ViewBag.listcategory = listcategory;

                return View();
            }
            return View();
        }

        [HttpPost("/Login")]

        public IActionResult Login(string email, string password, string returnUrl)
        {
            Users user = new Users();
            user = userService.Login(email, password);
            if (user == null)
            {
                ViewBag.Error = "Wrong Username/Email";
                ViewBag.Error1 = "Wrong Password";
                return View("Login");
            }
            else
            {
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetInt32("IsSeller", user.IsSeller);
                ViewBag.Notification = true;
                if (user.Status == 0)
                {
                    ViewBag.Error = "Account locked";
                    if (user.UserLevel == 1)
                    {
                        return Redirect("/Admin/Dashboard");
                    }
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("/");
                }
            }
            ViewBag.Accountlocked = "Account locked";
            return View("Login");
        }
        [HttpGet("/Login")]
        public IActionResult Login(string returnUrl)
        {
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();
            var userId = HttpContext.Session.GetInt32("UserId");
            Users user = userService.GetUsersByID(userId);
            if (userId != null)
            {
                if (user.Status == 1)
                {
                    // ViewBag.Accountlocked = "Account locked";
                }
            }
            else
            {
                // ViewBag.Error = "Wrong Username/email";
                // ViewBag.Error1 = "Wrong Passwowrd";
            }

            if (returnUrl != null)
            {
                ViewBag.returnUrl = returnUrl;
                var myEncodedString = System.Net.WebUtility.UrlDecode(returnUrl);
                Console.WriteLine(myEncodedString);
            }

            return View();
        }
        [HttpPost("/BecomeSeller")]
        public IActionResult BecomeSeller(Seller seller1, Languages languages, Category category, Skills skills)
        {

            Console.WriteLine(category.CategoryName);
            int? userId = HttpContext.Session.GetInt32("UserId");
            Users users = dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            var category1 = dbContext.Category.FirstOrDefault(cat => cat.CategoryName == category.CategoryName);

            Console.WriteLine(languages.Level);
            var seller = userService.BecomeSeller(users, languages, seller1, category1, skills);
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();
            if (seller != null)
            {
                seller = dbContext.Seller.FirstOrDefault(seller => seller.SellerId == seller1.SellerId);
                // Set Session lan 2
                HttpContext.Session.SetInt32("IsSeller", users.IsSeller);

                return Redirect("/Home/Index");
            }
            return View();
        }

        [HttpGet("/BecomeSeller")]
        public IActionResult BecomeSeller()
        {
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();

            if (listcategory != null)
            {
                List<Category> listSubCategory = new List<Category>();
                listSubCategory = adminService.GetListSubCategoryByCategoryParentId(1);

                ViewBag.subcategory = listSubCategory;
                ViewBag.listcategory = listcategory;
            }
            return View();
        }
        [HttpGet("/EditProfile")]
        // public IActionResult EditProfile()
        // {
        //     var userId = HttpContext.Session.GetInt32("UserId");
        //     var user = userService.GetUsersByID(userId);
        //     if (user != null)
        //     {
        //         ViewBag.UserId = userId;
        //         ViewBag.UserName = user.UserName;
        //         ViewBag.Email = user.Email;
        //         ViewBag.FullName = user.FullName;
        //         ViewBag.Country = user.Country;
        //         ViewBag.Address = user.Address;

        //     }
        //     return View();

        // }
        [HttpPost("User/UploadImage")]
        public bool UploadImage()
        {
            var newFileName = string.Empty;
            if (HttpContext.Request.Form.Files != null)
            {
                Console.WriteLine("tung dep trai");
                var fileName = string.Empty;
                string PathDB = string.Empty;

                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "Images/User/") + $@"\{newFileName}";

                        // if you want to store path of folder in database
                        PathDB = "Images/User/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                        int? UserId = HttpContext.Session.GetInt32("UserId");

                        bool check = userService.UploadAvater(UserId, PathDB);
                        if (check)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;

        }

        // [HttpGet("/BecomeSeller")]
        // public IActionResult BecomeSeller()
        // {
        //     List<Category> listcategory = new List<Category>();
        //     listcategory = adminService.GetListCategoryBy();

        //     if (listcategory != null)
        //     {
        //         List<Category> listSubCategory = new List<Category>();
        //         listSubCategory = adminService.GetListSubCategoryByCategoryParentId(1);

        //         ViewBag.subcategory = listSubCategory;
        //         ViewBag.listcategory = listcategory;
        //     }
        //     return View();
        // }
        // [HttpGet("/EditProfile")]
        // public IActionResult EditProfile()
        // {
        //     var userId = HttpContext.Session.GetInt32("UserId");
        //     var user = userService.GetUsersByID(userId);
        //     if (user != null)
        //     {
        //         ViewBag.UserId = userId;
        //         ViewBag.UserName = user.UserName;
        //         ViewBag.Email = user.Email;
        //         ViewBag.FullName = user.FullName;
        //         ViewBag.Country = user.Country;
        //         ViewBag.Address = user.Address;

        //     }
        //     return View();
        // }
        // [Route("{username}")]
        // [HttpGet]
        // public IActionResult ProfileSeller(string username)
        // {
        //     if (username == null)
        //     {
        //         return Redirect("/");
        //     }
        //     Users users = userService.GetUserByUsername(username);
        //     if (users == null)
        //     {
        //         return Redirect("/");
        //     }
        //     if (users.UserLevel == 2)
        //     {
        //         return Redirect("/Seller/ProfileSeller");
        //     }

        //     Seller seller = userService.GetSellerByUserID(users.UserId);
        //     List<Service> services = new List<Service>();
        //     services = gigService.GetServicesBySellerId(seller.SellerId);
        //     if (seller == null)
        //     {
        //         return Redirect("/");
        //     }
        //     foreach (var item in services)
        //     {
        //         item.ListImage = adminService.GetListImageService(item.ServiceId);
        //     }
        //     ViewBag.sellerprofile = seller;
        //     ViewBag.userProfile = users;
        //     ViewBag.listServiceProfile = services;

        //     return View(services);
        // }


    }
}