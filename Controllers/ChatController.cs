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
  public class ChatController : Controller
  {
    public ChatController()
    {

    }
    [HttpGet("/conversation/inbox")]
    public IActionResult inbox()
    {
      return View();
    }
  }
}