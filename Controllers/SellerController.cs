
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Services;
using Microsoft.AspNetCore.Http;

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