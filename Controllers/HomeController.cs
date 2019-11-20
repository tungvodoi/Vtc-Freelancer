﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using Microsoft.AspNetCore.Http;
using Vtc_Freelancer.ActionFilter;

namespace Vtc_Freelancer.Controllers
{
  [Authentication]
  public class HomeController : Controller
  {
    private UserService userService;
    private AdminService adminService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, UserService userService, AdminService adminService)
    {
      this.userService = userService;
      this.adminService = adminService;
      _logger = logger;
    }

    public IActionResult Index()
    {

      if (HttpContext.Session.GetInt32("UserId") != null)
      {
        int? userId = HttpContext.Session.GetInt32("UserId");
        Users userads = userService.GetUsersByID(userId);
        ViewBag.UserName = userads.UserName;

        List<Category> listcategory = new List<Category>();
        listcategory = adminService.GetListCategoryBy();

        if (listcategory != null)
        {
          ViewBag.listcategory = listcategory;

          return View();
        }


        return View();
      }
      return Redirect("/Login");
    }
    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return Redirect("/");
    }
    public IActionResult EditProfile()
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      Users userads = userService.GetUsersByID(userId);
      ViewBag.UserName = userads.UserName;
      return View();
    }

  }
}

