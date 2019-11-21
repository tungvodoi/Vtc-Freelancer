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
using Vtc_Freelancer.ActionFilter;

namespace Vtc_Freelancer.Controllers
{
  // [Authentication]
  public class SellerController : Controller
  {
    private UserService userService;
    private readonly ILogger<HomeController> _logger;

    public SellerController(ILogger<HomeController> logger, UserService userService)
    {
      this.userService = userService;
      _logger = logger;
    }

    public IActionResult Index()
    {
      ViewBag.UserName = HttpContext.Session.GetString("UserName");
      return View();
    }

  }
}