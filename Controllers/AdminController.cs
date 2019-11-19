using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using System;
using System.Collections.Generic;

namespace Vtc_Freelancer.Controllers
{
  public class AdminController : Controller
  {
    private MyDbContext dbContext;
    private HashPassword hashPassword;
    // private static Users user;
    private AdminService adminService;
    public AdminController(MyDbContext dbContext, HashPassword hashPassword, AdminService adminService)
    {
      this.dbContext = dbContext;
      this.hashPassword = hashPassword;
      this.adminService = adminService;
      dbContext.Database.EnsureCreated();
    }
    [HttpGet("/CreateCategory")]
    public IActionResult CreateCategory()
    {
      return View();
    }
    [HttpPost("/CreateCategory")]
    public IActionResult CreateCategory(string CategoryName, int ParentId, string SubCategory)
    {
      // bool add = 
      if (adminService.CreateCategory(CategoryName, ParentId, SubCategory))
      {
        return Redirect("/");
      }
      return View();
    }
    public IActionResult EditCategory(Category category)
    {
      if (adminService.EditCategory(category))
      {
        return Redirect("/");
      }
      return View();

    }
    // [HttpGet("/GetListCategoryByParentId")]
    // public IActionResult GetListCategoryByParentId()
    // {

    //   return View();
    // }
    // [HttpPost("/GetListCategoryByParentId")]
    // public IActionResult GetListCategoryByParentId(string CategoryName)
    // {
    //   List<Category> category = adminService.GetListCategoryByParentId(CategoryName);
    //   ViewBag.ListCategory = category;
    //   return RedirectToAction("Index", "Home", new { ListCategory = category });
    // }
  }

}