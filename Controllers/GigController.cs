using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;

namespace Vtc_Freelancer.Controllers
{
    // [Authentication]
    public class GigController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private GigService gigService;
        private UserService userService;
        private AdminService adminService;
        public GigController(GigService gigService, UserService userService, AdminService adminService, IHostingEnvironment IHostingEnvironment)
        {
            _environment = IHostingEnvironment;
            this.gigService = gigService;
            this.userService = userService;
            this.adminService = adminService;
        }

        [HttpGet("/CreateService/Step1")]
        public IActionResult Step1()
        {
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();
            int? userId = HttpContext.Session.GetInt32("UserId");
            Users userads = userService.GetUsersByID(userId);
            ViewBag.userAvatar = userads.Avatar;
            if (listcategory != null)
            {
                List<Category> listSubCategory = new List<Category>();
                listSubCategory = adminService.GetListSubCategoryByCategoryParentId(1);
                ViewBag.subcategory = listSubCategory;
                ViewBag.listcategory = listcategory;
            }
            return View();
        }

        [HttpGet("/CreateService/Step2")]
        public IActionResult Step2()
        {
            StreamReader sr = new StreamReader("./Common/config.json");
            // Read the stream to a string, and write the string to the console.
            String line = sr.ReadToEnd();
            JObject json = JObject.Parse(line);
            sr.Close();
            ViewBag.json = json;
            int? userId = HttpContext.Session.GetInt32("UserId");
            Users userads = userService.GetUsersByID(userId);
            ViewBag.userAvatar = userads.Avatar;
            return View();
        }


        [HttpGet("/CreateService/Step3")]
        public IActionResult Step3()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            Users userads = userService.GetUsersByID(userId);
            ViewBag.userAvatar = userads.Avatar;
            return View();
        }


        [HttpGet("/CreateService/Step4")]
        public IActionResult Step4()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            Users userads = userService.GetUsersByID(userId);
            ViewBag.userAvatar = userads.Avatar;
            return View();
        }


        [HttpPost("/CreateService/CreateServiceStep1")]
        public IActionResult CreateServiceStep1(string title, string category, string subcategory, string tags)
        {


            int? userID = HttpContext.Session.GetInt32("UserId");
            int SellerID = userService.GetSellerByUserID(userID).SellerId;
            int ServiceId = gigService.CreateServiceStepOne(title, category, subcategory, tags, SellerID);
            if (ServiceId == 0)
            {
                return Redirect("/CreateService/Step1");
            }
            HttpContext.Session.SetInt32("serviceId", ServiceId);
            return Redirect("/CreateService/Step2");
        }

        [HttpPost("/CreateService/CreateServiceStep2")]
        public IActionResult CreateServiceStep2(string basicTitle, string standardTitle, string premiumTitle, string basicDescription, string standardDescription, string premiumDescription, int basicDelivery, int standardDelivery, int premiumDelivery, int basicRevision, int standardRevision, int premiumRevision, double basicPrice, double standardPrice, double premiumPrice)
        {
            int? ServiceId = HttpContext.Session.GetInt32("serviceId");
            Service service = gigService.GetServiceByID(ServiceId);
            if (service != null)
            {
                Package pacBasic = new Package();
                pacBasic.Name = "Basic";
                pacBasic.Title = basicTitle;
                pacBasic.Description = basicDescription;
                pacBasic.DeliveryTime = basicDelivery;
                pacBasic.NumberRevision = basicRevision;
                pacBasic.Price = basicPrice;
                pacBasic.ServiceId = service.ServiceId;
                bool check = gigService.CreateServiceStepTwo(pacBasic);
                if (standardTitle != null && premiumTitle != null && standardDescription != null && premiumDescription != null)
                {
                    Package pacStandard = new Package();
                    pacStandard.Name = "Standard";
                    pacStandard.Title = standardTitle;
                    pacStandard.Description = standardDescription;
                    pacStandard.DeliveryTime = standardDelivery;
                    pacStandard.NumberRevision = standardRevision;
                    pacStandard.Price = standardPrice;
                    pacStandard.ServiceId = service.ServiceId;
                    check = gigService.CreateServiceStepTwo(pacStandard);
                    Package pacPremium = new Package();
                    pacPremium.Name = "Premium";
                    pacPremium.Title = premiumTitle;
                    pacPremium.Description = premiumDescription;
                    pacPremium.DeliveryTime = premiumDelivery;
                    pacPremium.NumberRevision = premiumRevision;
                    pacPremium.Price = premiumPrice;
                    pacPremium.ServiceId = service.ServiceId;
                    check = gigService.CreateServiceStepTwo(pacPremium);
                }
                if (check)
                {
                    return Redirect("/CreateService/Step3");
                }
            }
            return Redirect("/CreateService/Step2");
        }

        [HttpPost("/CreateService/CreateServiceStep3")]
        public IActionResult CreateServiceStep3(string serviceDescription, string question, string reply)
        {
            int? ServiceId = HttpContext.Session.GetInt32("serviceId");
            bool check = gigService.CreateServiceStepThree(ServiceId, serviceDescription, question, reply);
            if (check)
            {
                return Redirect("/CreateService/Step4");
            }
            return Redirect("/CreateService/Step3");
        }
        [HttpPost("/CreateService/Step4")]
        public IActionResult Step4(string name)
        {
            List<string> urlImages = new List<string>();
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
                        fileName = Path.Combine(_environment.WebRootPath, "Images/Gigs/") + $@"\{newFileName}";

                        // if you want to store path of folder in database
                        PathDB = "Images/Gigs/" + newFileName;
                        urlImages.Add(PathDB);

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
            }
            int? ServiceId = HttpContext.Session.GetInt32("serviceId");
            bool check = gigService.CreateServiceStepFour(ServiceId, urlImages);

            if (check)
            {
                return Redirect("/");
            }
            return Redirect("/CreateService/Step4");
        }
        [HttpGet("/Gig/Report")]
        public IActionResult reportGig(int UserId, int ServiceId, string titleReport, string contentReport)
        {
            if (gigService.reportGig(UserId, ServiceId, titleReport, contentReport))
            {
                ViewBag.ReportStatus = true;
            }
            else
            {
                ViewBag.ReportStatus = false;
            }
            return View();
        }

        [HttpGet("Gig/ServiceDetail")]
        public IActionResult ServiceDetail(int? serviceId)
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
            if (serviceId == null)
            {
                return Redirect("/");
            }
            else
            {
                if (HttpContext.Session.GetInt32("UserId") != null)
                {
                    int? userId = HttpContext.Session.GetInt32("UserId");
                    Users userads = userService.GetUsersByID(userId);
                    ViewBag.UserName = userads.UserName;
                    ViewBag.userAvatar = userads.Avatar;

                    ViewBag.IsSeller = HttpContext.Session.GetInt32("IsSeller");
                    ViewBag.SellerId = HttpContext.Session.GetInt32("SellerId");
                }
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                Service service = new Service();
                service = gigService.GetServiceByID(serviceId);
                if (service == null)
                {
                    return Redirect("/");
                }
                ViewBag.serviceDetail = service;
                Users users = new Users();
                users = gigService.GetUserByServiceId(serviceId);
                List<ImageService> images = new List<ImageService>();
                images = gigService.GetListImagesByServiceId(serviceId);
                List<Package> ListPackage = gigService.GetPackageByServiceID(serviceId);
                foreach (var item in ListPackage)
                {
                    if (item.Name == "Premium")
                    {
                        ViewBag.PackagePremium = gigService.GetPackageByPackageID(item.PackageId);
                    }
                    else if (item.Name == "Standard")
                    {
                        ViewBag.PackageStandard = gigService.GetPackageByPackageID(item.PackageId);
                    }
                    else
                    {
                        ViewBag.PackageBasic = gigService.GetPackageByPackageID(item.PackageId);
                    }
                }
                ViewBag.ImageService = images;
                ViewBag.serviceDetailUser = users;
                // ViewBag.UserName = users.UserName;
                // ViewBag.IsSeller = users.IsSeller;
                return View();
            }
        }
    }
}