using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Vtc_Freelancer.ActionFilter;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;

namespace Vtc_Freelancer.Controllers
{
  [Authentication]
  public class GigController : Controller
  {
    private readonly IHostingEnvironment _environment;
    private GigService gigService;
    private UserService userService;
    private OrderService orderService;
    private AdminService adminService;
    public GigController(GigService gigService, UserService userService, AdminService adminService, OrderService orderService, IHostingEnvironment IHostingEnvironment)
    {
      _environment = IHostingEnvironment;
      this.gigService = gigService;
      this.userService = userService;
      this.orderService = orderService;
      this.adminService = adminService;
    }

    [HttpGet("/CreateService/Step1")]
    public IActionResult Step1()
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
      listcategory = new List<Category>();
      listcategory = adminService.GetListCategoryBy();
      int? userId = HttpContext.Session.GetInt32("UserId");
      Users userads = userService.GetUserByUserId(userId);
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
      StreamReader sr = new StreamReader("./Common/config.json");
      // Read the stream to a string, and write the string to the console.
      String line = sr.ReadToEnd();
      JObject json = JObject.Parse(line);
      sr.Close();
      ViewBag.json = json;
      int? userId = HttpContext.Session.GetInt32("UserId");
      Users userads = userService.GetUserByUserId(userId);
      ViewBag.userAvatar = userads.Avatar;
      return View();
    }

    [HttpGet("/CreateService/Step3")]
    public IActionResult Step3()
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
      int? userId = HttpContext.Session.GetInt32("UserId");
      Users userads = userService.GetUserByUserId(userId);
      ViewBag.userAvatar = userads.Avatar;
      return View();
    }

    [HttpGet("/CreateService/Step4")]
    public IActionResult Step4()
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
      int? userId = HttpContext.Session.GetInt32("UserId");
      Users userads = userService.GetUserByUserId(userId);
      ViewBag.userAvatar = userads.Avatar;
      return View();
    }
    [HttpGet("/CreateService/Step5")]
    public IActionResult Step5()
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
      int? userId = HttpContext.Session.GetInt32("UserId");
      Users userads = userService.GetUserByUserId(userId);
      ViewBag.userAvatar = userads.Avatar;
      return View();
    }

    [HttpPost("/CreateService/CreateServiceStep1")]
    public IActionResult CreateServiceStep1(string title, string category, string subcategory, string tag)
    {


      int? userID = HttpContext.Session.GetInt32("UserId");
      int SellerID = userService.GetSellerByUserID(userID).SellerId;
      int ServiceId = gigService.CreateServiceStepOne(title, category, subcategory, tag, SellerID);
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
    [HttpPost("/CreateService/CreateServiceStep4")]
    public IActionResult CreateServiceStep4(string name)
    {
      List<string> urlImages = new List<string>();
      var newFileName = string.Empty;
      if (HttpContext.Request.Form.Files != null)
      {
        Users user = userService.GetUserByUserId(HttpContext.Session.GetInt32("UserId"));
        if (user != null)
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
              fileName = Path.Combine(_environment.WebRootPath, $@"Images/Gigs/{user.UserName}") + $@"\{newFileName}";

              // if you want to store path of folder in database
              PathDB = $@"Images/Gigs/{user.UserName}/" + newFileName;
              urlImages.Add(PathDB);

              if (Directory.Exists($@"Images/Gigs/{user.UserName}"))
              {
                using (FileStream fs = System.IO.File.Create(fileName))
                {
                  file.CopyTo(fs);
                  fs.Flush();
                }
              }
              else
              {
                DirectoryInfo di = Directory.CreateDirectory($@"wwwroot/Images/Gigs/{user.UserName}");
                using (FileStream fs = System.IO.File.Create(fileName))
                {
                  file.CopyTo(fs);
                  fs.Flush();
                }
              }
            }
          }
        }
      }
      int? ServiceId = HttpContext.Session.GetInt32("serviceId");
      bool check = gigService.CreateServiceStepFour(ServiceId, urlImages);

      if (check)
      {
        return Redirect("/CreateService/Step5");
      }
      return Redirect("/CreateService/Step4");
    }
    [HttpPost("/CreateService/CreateServiceStep5")]
    public IActionResult CreateServiceStep5()
    {
      int? ServiceId = HttpContext.Session.GetInt32("serviceId");
      bool check = gigService.CreateServiceStepFive(ServiceId);
      if (check)
      {
        return Redirect("/manage_gig/active");
      }
      return Redirect("/CreateService/Step5");
    }
    [HttpGet("/manage_gig/pauseservice")]
    public IActionResult pauseservice(int? serviceId)
    {
      gigService.ChangeStatusService(serviceId, 2);
      return Redirect("/manage_gig/pause");
    }
    [HttpGet("/manage_gig/activeservice")]
    public IActionResult activeservice(int? serviceId)
    {
      gigService.ChangeStatusService(serviceId, 1);
      return Redirect("/manage_gig/active");
    }
    [HttpGet("/manage_gig/deleteservice")]
    public IActionResult deleteservice(int? serviceId)
    {
      gigService.ChangeStatusService(serviceId, 6);
      return Redirect("/manage_gig/active");
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
    [HttpGet("/manage_gig/active")]
    public IActionResult active()
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      Users userads = userService.GetUserByUserId(userId);
      ViewBag.userAvatar = userads.Avatar;
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
      Seller seller = userService.GetSellerByUserID(HttpContext.Session.GetInt32("UserId"));
      List<Service> listService = gigService.GetServicesBySellerId(seller.SellerId);
      foreach (var item in listService)
      {
        item.ListImage = adminService.GetListImageService(item.ServiceId);
        item.listPackage = gigService.GetPackageByServiceID(item.ServiceId);
      }
      ViewBag.listServiceProfile = listService;
      ViewBag.CountActive = gigService.GetCountServiceStatus(seller.SellerId, 1);
      ViewBag.CountDraft = gigService.GetCountServiceStatus(seller.SellerId, 0);
      ViewBag.CountPause = gigService.GetCountServiceStatus(seller.SellerId, 2);
      ViewBag.CountDenied = gigService.GetCountServiceStatus(seller.SellerId, 3);
      ViewBag.CountRequire = gigService.GetCountServiceStatus(seller.SellerId, 4);
      ViewBag.CountPending = gigService.GetCountServiceStatus(seller.SellerId, 5);
      return View();
    }
    [HttpGet("/manage_gig/draft")]
    public IActionResult draft()
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      Users userads = userService.GetUserByUserId(userId);
      ViewBag.userAvatar = userads.Avatar;
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
      Seller seller = userService.GetSellerByUserID(HttpContext.Session.GetInt32("UserId"));
      List<Service> listService = gigService.GetServicesBySellerId(seller.SellerId);
      foreach (var item in listService)
      {
        item.ListImage = adminService.GetListImageService(item.ServiceId);
        item.listPackage = gigService.GetPackageByServiceID(item.ServiceId);
      }
      ViewBag.listServiceProfile = listService;
      ViewBag.CountActive = gigService.GetCountServiceStatus(seller.SellerId, 1);
      ViewBag.CountDraft = gigService.GetCountServiceStatus(seller.SellerId, 0);
      ViewBag.CountPause = gigService.GetCountServiceStatus(seller.SellerId, 2);
      ViewBag.CountDenied = gigService.GetCountServiceStatus(seller.SellerId, 3);
      ViewBag.CountRequire = gigService.GetCountServiceStatus(seller.SellerId, 4);
      ViewBag.CountPending = gigService.GetCountServiceStatus(seller.SellerId, 5);
      return View();
    }

    [HttpGet("/manage_gig/pause")]
    public IActionResult pause()
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      Users userads = userService.GetUserByUserId(userId);
      ViewBag.userAvatar = userads.Avatar;
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
      Seller seller = userService.GetSellerByUserID(HttpContext.Session.GetInt32("UserId"));
      List<Service> listService = gigService.GetServicesBySellerId(seller.SellerId);
      foreach (var item in listService)
      {
        item.ListImage = adminService.GetListImageService(item.ServiceId);
        item.listPackage = gigService.GetPackageByServiceID(item.ServiceId);
      }
      ViewBag.listServiceProfile = listService;
      ViewBag.CountActive = gigService.GetCountServiceStatus(seller.SellerId, 1);
      ViewBag.CountDraft = gigService.GetCountServiceStatus(seller.SellerId, 0);
      ViewBag.CountPause = gigService.GetCountServiceStatus(seller.SellerId, 2);
      ViewBag.CountDenied = gigService.GetCountServiceStatus(seller.SellerId, 3);
      ViewBag.CountRequire = gigService.GetCountServiceStatus(seller.SellerId, 4);
      ViewBag.CountPending = gigService.GetCountServiceStatus(seller.SellerId, 5);
      return View();
    }
    [HttpGet("/manage_gig/denied")]
    public IActionResult denied()
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      Users userads = userService.GetUserByUserId(userId);
      ViewBag.userAvatar = userads.Avatar;
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
      Seller seller = userService.GetSellerByUserID(HttpContext.Session.GetInt32("UserId"));
      List<Service> listService = gigService.GetServicesBySellerId(seller.SellerId);
      foreach (var item in listService)
      {
        item.ListImage = adminService.GetListImageService(item.ServiceId);
        item.listPackage = gigService.GetPackageByServiceID(item.ServiceId);
      }
      ViewBag.listServiceProfile = listService;
      ViewBag.CountActive = gigService.GetCountServiceStatus(seller.SellerId, 1);
      ViewBag.CountDraft = gigService.GetCountServiceStatus(seller.SellerId, 0);
      ViewBag.CountPause = gigService.GetCountServiceStatus(seller.SellerId, 2);
      ViewBag.CountDenied = gigService.GetCountServiceStatus(seller.SellerId, 3);
      ViewBag.CountRequire = gigService.GetCountServiceStatus(seller.SellerId, 4);
      ViewBag.CountPending = gigService.GetCountServiceStatus(seller.SellerId, 5);
      return View();
    }
    [HttpGet("/manage_gig/requires_mod")]
    public IActionResult requires_mod()
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      Users userads = userService.GetUserByUserId(userId);
      ViewBag.userAvatar = userads.Avatar;
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
      Seller seller = userService.GetSellerByUserID(HttpContext.Session.GetInt32("UserId"));
      List<Service> listService = gigService.GetServicesBySellerId(seller.SellerId);
      foreach (var item in listService)
      {
        item.ListImage = adminService.GetListImageService(item.ServiceId);
        item.listPackage = gigService.GetPackageByServiceID(item.ServiceId);
      }
      ViewBag.listServiceProfile = listService;
      ViewBag.CountActive = gigService.GetCountServiceStatus(seller.SellerId, 1);
      ViewBag.CountDraft = gigService.GetCountServiceStatus(seller.SellerId, 0);
      ViewBag.CountPause = gigService.GetCountServiceStatus(seller.SellerId, 2);
      ViewBag.CountDenied = gigService.GetCountServiceStatus(seller.SellerId, 3);
      ViewBag.CountRequire = gigService.GetCountServiceStatus(seller.SellerId, 4);
      ViewBag.CountPending = gigService.GetCountServiceStatus(seller.SellerId, 5);
      return View();
    }
    [HttpGet("/manage_gig/pending")]
    public IActionResult pending()
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      Users userads = userService.GetUserByUserId(userId);
      ViewBag.userAvatar = userads.Avatar;
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
      Seller seller = userService.GetSellerByUserID(HttpContext.Session.GetInt32("UserId"));
      List<Service> listService = gigService.GetServicesBySellerId(seller.SellerId);
      foreach (var item in listService)
      {
        item.ListImage = adminService.GetListImageService(item.ServiceId);
        item.listPackage = gigService.GetPackageByServiceID(item.ServiceId);
      }
      ViewBag.listServiceProfile = listService;
      ViewBag.CountActive = gigService.GetCountServiceStatus(seller.SellerId, 1);
      ViewBag.CountDraft = gigService.GetCountServiceStatus(seller.SellerId, 0);
      ViewBag.CountPause = gigService.GetCountServiceStatus(seller.SellerId, 2);
      ViewBag.CountDenied = gigService.GetCountServiceStatus(seller.SellerId, 3);
      ViewBag.CountRequire = gigService.GetCountServiceStatus(seller.SellerId, 4);
      ViewBag.CountPending = gigService.GetCountServiceStatus(seller.SellerId, 5);
      return View();
    }
    [HttpGet("/Gig/ServiceDetail")]
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
          Users userads = userService.GetUserByUserId(userId);
          ViewBag.UserName = userads.UserName;
          ViewBag.userAvatar = userads.Avatar;
          ViewBag.ListOrder = orderService.GetListOrderbyUserId(userId);
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