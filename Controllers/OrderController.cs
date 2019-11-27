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
    public OrderController(UserService userService, OrderService orderService)
    {
      this.userService = userService;
      this.orderService = orderService;
    }
    [HttpGet("/Customize/Order")]
    public IActionResult Order()
    {
      Package pac = orderService.GetPackageByID(1);
      if (pac != null)
      {
        pac.Service = orderService.GetServiceByID(pac.ServiceId);
      }
      return View(pac);
    }
  }
}