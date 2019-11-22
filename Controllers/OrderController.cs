using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using System.Linq;
using System.Collections.Generic;

namespace Vtc_Freelancer.Controllers
{
  public class OrderController : Controller
  {
    private UserService userService;
    private OrderService orderService;
    private GigService gigService;
    public OrderController(UserService userService, OrderService orderService, GigService gigService)
    {
      this.userService = userService;
      this.orderService = orderService;
      this.gigService = gigService;
    }
    [HttpGet("/customize/order")]
    public IActionResult Order()
    {
      Package pac = gigService.GetPackageByID(2);
      if (pac != null)
      {
        pac.Service = gigService.GetServiceByID(pac.ServiceId);
      }
      return View(pac);
    }

  }
}