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
    private static Users user;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, UserService userService)
    {
      this.userService = userService;
      _logger = logger;
    }
    // public IActionResult Login(string email, string password)
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
      int? userId = HttpContext.Session.GetInt32("UserId");
      // Console.WriteLine(userId);
      Users userads = userService.GetUsersByID(userId);
      ViewBag.UserName = userads.UserName;
      return View();
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
      var UserId = HttpContext.Session.GetInt32("UserId");
      ViewBag.UserId = UserId;
      var UserName = HttpContext.Session.GetString("UserName");
      ViewBag.UserName = UserName;
      return View();
    }


  }
}
