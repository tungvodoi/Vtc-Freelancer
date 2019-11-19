using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using Microsoft.AspNetCore.Http;

namespace Vtc_Freelancer.Controllers
{
  public class HomeController : Controller
  {
    // private MyDbContext dbContext;
    private UserService userService;
    private AdminService adminService;
    public static List<Category> category;
    private static Users user;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, UserService userService, AdminService adminService)
    {
      this.userService = userService;
      this.adminService = adminService;
      _logger = logger;
    }
    // public IActionResult Login(Test test1)
    // {
    //   // if (test1.Username == "admin" && test1.Username1 == "admin")
    //   // {
    //   //   test1.Text = "Welcome Linh";
    //   // }
    //   // else
    //   // {
    //   //   test1.Text = "Mr.Hoang said : 'Djt nhau'";
    //   // }
    //   // return View(test1);
    //   HttpContext.Session.SetInt32("UserId", user.UserId);

    //   user = userService.Login(email, password);
    //   // HttpContext.Session.SetString("UserName", user.UserName);

    //   Console.WriteLine("1");
    //   Console.WriteLine(user.UserName);


    //   return View();
    // }
    public IActionResult Index()
    {

      // var UserId = HttpContext.Session.GetInt32("UserId");
      // ViewBag.UserId = UserId;

      // var UserName = HttpContext.Session.GetString("UserName");
      // ViewBag.UserName = UserName;
      // Console.WriteLine(UserName);
      if (category != null)
      {

        ViewBag.ListCategory = category;
      }
      if (HttpContext.Session.GetInt32("UserId") != null)
      {
        int? userId = HttpContext.Session.GetInt32("UserId");
        // Console.WriteLine(userId);
        Users userads = userService.GetUsersByID(userId);
        ViewBag.UserName = userads.UserName;
        return View();
      }
      return View();
    }
    [HttpGet("/GetListCategoryByParentId")]
    public IActionResult GetListCategoryByParentId()
    {

      return View();
    }
    [HttpPost("/GetListCategoryByParentId")]
    public IActionResult GetListCategoryByParentId(string categoryName)
    {

      // var UserId = HttpContext.Session.GetInt32("UserId");
      // ViewBag.UserId = UserId;

      // var UserName = HttpContext.Session.GetString("UserName");
      // ViewBag.UserName = UserName;
      // Console.WriteLine(UserName);
      category = adminService.GetListCategoryByParentId(categoryName);
      ViewBag.ListCategory = category;
      foreach (var item in ViewBag.ListCategory)
      {
        Console.WriteLine("haha " + item.CategoryName);
      }
      return Redirect("/");
    }

    public IActionResult Privacy()
    {
      return View();
    }
    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return Redirect("/");
    }
    public IActionResult EditProfile()
    {
      // HttpContext.Session.SetInt32("UserId", user.UserId);
      // HttpContext.Session.SetString("UserName", user.UserName);
      int? userId = HttpContext.Session.GetInt32("UserId");
      // Console.WriteLine(userId);
      Users userads = userService.GetUsersByID(userId);
      ViewBag.UserName = userads.UserName;
      // var UserId = HttpContext.Session.GetString("UserName");
      // ViewBag.UserId = UserId;
      // var UserName = HttpContext.Session.GetString("UserName");
      // ViewBag.UserName = UserName;
      return View();
    }


  }
}
