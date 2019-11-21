using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using Vtc_Freelancer.ActionFilter;

namespace Vtc_Freelancer.Controllers
{
  // [Authentication]
  public class AdminController : Controller
  {
    private UserService userService;
    private GigService gigService;
    private AdminService adminService;
    public AdminController(UserService userService, GigService gigService, AdminService adminService)
    {
      this.userService = userService;
      this.gigService = gigService;
      this.adminService = adminService;
    }

    [HttpGet("/Admin/Dashboard")]
    public IActionResult Dashboard()
    {
      return View();
    }
    [HttpGet("/Admin/ServiceActive")]
    public IActionResult ServiceActive()
    {
      return View();
    }
    [HttpGet("/Admin/ServiceInactive")]
    public IActionResult ServiceInactive()
    {

      return View();
    }
    [HttpGet("/Admin/Error")]
    public IActionResult Error()
    {
      return View();
    }
    [HttpGet("/Admin/Blank")]
    public IActionResult Blank()
    {
      return View();
    }
    [HttpGet("/Admin/Buttons")]
    public IActionResult Buttons()
    {
      return View();
    }
    [HttpGet("/Admin/Cards")]
    public IActionResult Cards()
    {
      return View();
    }
    [HttpGet("/Admin/Charts")]
    public IActionResult Charts()
    {
      return View();
    }
    [HttpGet("/Admin/Tables")]
    public IActionResult Tables()
    {
      return View();
    }
    [HttpGet("/Admin/UtilitiesAnimation")]
    public IActionResult UtilitiesAnimation()
    {
      return View();
    }
    [HttpGet("/Admin/UtilitiesBorder")]
    public IActionResult UtilitiesBorder()
    {
      return View();
    }
    [HttpGet("/Admin/UtilitiesColor")]
    public IActionResult UtilitiesColor()
    {
      return View();
    }
    [HttpGet("/Admin/UtilitiesOther")]
    public IActionResult UtilitiesOther()
    {
      return View();
    }
    [HttpPost("/CreateCategory")]
    public IActionResult CreateCategory(string CategoryName, int ParenId, string SubCategoryName)
    {
      bool category = adminService.CreateCategory(CategoryName, ParenId, SubCategoryName);
      if (category)
      {

        return View();

      }
      return Redirect("/");


    }
    [HttpGet("/CreateCategory")]
    public IActionResult CreateCategory()
    {
      return View();
    }
    public IActionResult GetListCategory()
    {
      List<Category> listcategory = new List<Category>();
      listcategory = adminService.GetListCategoryBy();

      if (listcategory != null)
      {
        ViewBag.listcategory = listcategory;
        return Redirect("/BecomeSeller");
      }
      return View("Index");
    }

  }

}
