using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using Vtc_Freelancer.ActionFilter;

namespace Vtc_Freelancer.Controllers
{
    // [Authentication]
    public class GigController : Controller
    {
        private GigService gigService;
        public GigController(GigService gigService)
        {
            this.gigService = gigService;
        }
        [HttpGet("/CreateService")]
        public IActionResult CreateService()
        {
            return View();
        }
        [HttpPost]
        public IActionResult reportGig(int UserId, int ServiceId, string titleReport, string contentReport)
        {
            if (gigService.reportGig(UserId, ServiceId, titleReport, contentReport))
            {
                ViewBag.ReportStatus = true;
            }
            else
            {
                ViewBag.ReportStatus = false;
            }
            return View();
        }
    }
}