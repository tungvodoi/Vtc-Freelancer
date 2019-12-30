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
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

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
            var userId = HttpContext.Session.GetInt32("UserId");
            return View();
        }
        [HttpPost("/Register")]
        public IActionResult Register(string username, string email, string password)
        {
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionstr = regex.Matches(username);
            if (matchCollectionstr.Count < username.Length)
            {
                ViewBag.Error = "Username must be numbers or letters";
                return View();
            }
            if (userService.GetUserByEmail(email) != null)
            {
                ViewBag.Error = "Email has been used to register";
                return View();
            }
            else if (userService.GetUserByUsername(username) != null)
            {
                ViewBag.Error = "Username has been used to register";
                return View();
            }
            else
            {
                if (userService.Register(username, email, password))
                {
                    ViewBag.Noti = "Register Successfully :)";
                }
                return Redirect("/Login");

            }
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
                    if (user.Email == "admin@gmail.com")
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
            Users user = userService.GetUserByUserId(userId);
            // if (userId != null)
            // {
            //     if (user.Status == 1)
            //     {
            //         // ViewBag.Accountlocked = "Account locked";
            //     }
            // }
            // else
            // {
            //     // ViewBag.Error = "Wrong Username/email";
            //     // ViewBag.Error1 = "Wrong Passwowrd";
            // }

            if (returnUrl != null)
            {
                ViewBag.returnUrl = returnUrl;
                var myEncodedString = System.Net.WebUtility.UrlDecode(returnUrl);
            }

            return View();
        }
        [HttpPost("/BecomeSeller")]
        public IActionResult BecomeSeller(Seller seller1, Languages languages, Category category, Skills skills)
        {

            int? userId = HttpContext.Session.GetInt32("UserId");
            Users users = dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            var category1 = dbContext.Category.FirstOrDefault(cat => cat.CategoryName == category.CategoryName);
            var seller = userService.BecomeSeller(users, languages, seller1, category1, skills);
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();
            if (seller != null)
            {
                seller = userService.GetSellerBySellerID(seller1.SellerId);
                // Set Session lan 2
                HttpContext.Session.SetInt32("IsSeller", users.IsSeller);
                return Redirect("/");
            }
            return Redirect("/BecomeSeller");
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
            Users user = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));
            if (user != null)
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
                            //Getting FileName
                            fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                            //Assigning Unique Filename (Guid)
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                            //Getting file Extension
                            var FileExtension = Path.GetExtension(fileName);

                            // concating  FileName + FileExtension
                            newFileName = myUniqueFileName + FileExtension;

                            // Combines two strings into a path.
                            fileName = Path.Combine(_environment.WebRootPath, $@"Images/User/{user.UserName}") + $@"\{newFileName}";

                            // if you want to store path of folder in database
                            PathDB = $@"Images/User/{user.UserName}/" + newFileName;

                            if (Directory.Exists($@"User/{user.UserName}"))
                            {
                                using (FileStream fs = System.IO.File.Create(fileName))
                                {
                                    file.CopyTo(fs);
                                    fs.Flush();
                                }
                            }
                            else
                            {
                                DirectoryInfo di = Directory.CreateDirectory($@"wwwroot/Images/User/{user.UserName}");
                                using (FileStream fs = System.IO.File.Create(fileName))
                                {
                                    file.CopyTo(fs);
                                    fs.Flush();
                                }
                            }


                            bool check = userService.UploadAvater(user.UserId, PathDB);
                            if (check)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        [HttpGet("/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost("/ChangePassword")]
        public IActionResult ChangePassword(string currentpassword, string newpassword, string repassword)
        {
            MD5 md5Hash = MD5.Create();
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = userService.GetUserByUserId(userId);
            if (userService.VerifyMd5Hash(md5Hash, currentpassword, user.Password) != true)
            {
                ViewBag.Error = "Current password invalid";
                return View();
            }
            else
            {
                if (userService.ChangePassword(currentpassword, newpassword, repassword, userId))
                {
                    return Redirect("/");
                }
                return View();
            }

        }
        [HttpGet("/BillInformation")]
        public IActionResult BillInformation()
        {

            return View();
        }
        [HttpPost("/BillInformation")]
        public IActionResult BillInformation(string FullName, string Country, string Address)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            Regex regex = new Regex("[a-zA-Z]");
            MatchCollection matchCollectionstr = regex.Matches(FullName);

            if (matchCollectionstr.Count < FullName.Length)
            {
                ViewBag.Error = "FullName must be letters";
                return View();
            }
            else
            {
                if (userService.BillingInformation(FullName, Country, Address, userId))
                {
                    return Redirect("/");
                }
                return View();
            }


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