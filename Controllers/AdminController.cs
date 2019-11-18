using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;

namespace Vtc_Freelancer.Controllers
{
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