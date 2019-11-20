using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Vtc_Freelancer.Controllers
{
  public class UserController : Controller
  {
    private MyDbContext dbContext;
    private HashPassword hashPassword;
    private static Users user;
    private UserService userService;
    private AdminService adminService;
    public UserController(MyDbContext dbContext, HashPassword hashPassword, UserService userService, AdminService adminService)
    {
      this.dbContext = dbContext;
      this.hashPassword = hashPassword;
      this.userService = userService;
      this.adminService = adminService;
      dbContext.Database.EnsureCreated();
    }
    // public IActionResult Index()
    // {
    //   var userId = HttpContext.Session.GetInt32("UserId");
    //   return View();
    // }
    [HttpPost("/Register")]
    public IActionResult Register(string username, string email, string password)
    {
      if (userService.Register(username, email, password))
      {
        ViewBag.Noti = true;
      }
      return Redirect("/Login");
    }
    [HttpGet("/Register")]
    public IActionResult Register()
    {
      return View();
    }
    [HttpPost("/Login")]

    public IActionResult Login(string email, string password)
    {
      user = new Users();
      user = userService.Login(email, password);
      // HttpContext.Session.SetString("UserName", user.UserName);
      // Console.WriteLine("1");
      // Console.WriteLine(user.UserId);
      // Console.WriteLine(user.UserName);
      if (user == null)
      {
        return Redirect("/Login");
      }
      HttpContext.Session.SetInt32("UserId", user.UserId);
      ViewBag.Notification = true;
      return Redirect("/");
    }
    [HttpGet("/Login")]
    public IActionResult Login()
    {
      return View();
    }
    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return Redirect("/");
    }
    [HttpPost("/EditProfile")]
    public IActionResult EditProfile(int Id, string UserName, string Email)
    {
      bool Edit = userService.EditProfile(Id, UserName, Email);
      if (Edit == true)
      {
        return Redirect("/");
      }
      return View();
    }
    [HttpGet("/EditProfile")]
    public IActionResult EditProfile()
    {

      return View();
    }
    [HttpPost("/BecomeSeller")]
    public IActionResult BecomeSeller(Seller seller1, Languages languages, Category category, Skills skills)
    {
      Console.WriteLine(77777777777);
      Console.WriteLine(category.CategoryName);
      int? userId = HttpContext.Session.GetInt32("UserId");
      Users users = dbContext.Users.FirstOrDefault(u => u.UserId == userId);
      var category1 = dbContext.Category.FirstOrDefault(cat => cat.CategoryName == category.CategoryName);

      Console.WriteLine(languages.Level);
      var seller = userService.BecomeSeller(users, languages, seller1, category1, skills);

      if (seller != null)
      {
        seller = dbContext.Seller.FirstOrDefault(seller => seller.SellerId == seller1.SellerId);

        // HttpContext.Session.SetInt32("SellerId", seller.SellerId);
        List<Category> listcategory = new List<Category>();
        listcategory = adminService.GetListCategoryBy();

        if (listcategory != null)
        {
          ViewBag.listcategory = listcategory;

          return Redirect("/Home/Index");
        }


        return View("/Home/Index");
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
        ViewBag.listcategory = listcategory;

        return View("BecomeSeller");
      }
      return View();

    }


  }
}