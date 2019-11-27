
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Vtc_Freelancer.Models;

namespace Vtc_Freelancer.Controllers
{
  // [Authentication]
  public class SellerController : Controller
  {
    private UserService userService;
    private AdminService adminService;
    private readonly ILogger<HomeController> _logger;

    public SellerController(ILogger<HomeController> logger, UserService userService, AdminService adminService)
    {
      this.userService = userService;
      this.adminService = adminService;
      _logger = logger;
    }

    public IActionResult Index()
    {
      List<Category> listcategory = new List<Category>();
      listcategory = adminService.GetListCategoryBy();

      ViewBag.UserName = HttpContext.Session.GetString("UserName");
      if (listcategory != null)
      {
        ViewBag.listcategory = listcategory;

        return View();
      }
      return View();
    }
  }
}