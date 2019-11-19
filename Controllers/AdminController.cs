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
        public AdminController(UserService userService, GigService gigService)
        {
            this.userService = userService;
            this.gigService = gigService;
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
    }
}