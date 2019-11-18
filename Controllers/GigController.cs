using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
// using Vtc_Freelancer.Services;
using System;

namespace Vtc_Freelancer.Controllers
{
    public class GigController : Controller
    {
        // private GigService gigService;
        // public GigController(GigService gigService)
        // {
        //     this.gigService = gigService;
        // }
        [HttpGet("/CreateService")]
        public IActionResult CreateService()
        {
            return View();
        }
        // public IActionResult reportGig(int UserId, int ServiceId, string titleReport, string contentReport)
        // {
        //     if (gigService.reportGig(UserId, ServiceId, titleReport, contentReport))
        //     {
        //         return View();
        //     }
        //     else
        //     {
        //         return View();
        //     }
        // }
    }
}