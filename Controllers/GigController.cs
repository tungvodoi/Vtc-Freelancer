using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.ActionFilter;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;

namespace Vtc_Freelancer.Controllers {
    // [Authentication]
    public class GigController : Controller {
        private GigService gigService;
        public GigController (GigService gigService) {
            this.gigService = gigService;
        }

        [HttpGet ("/CreateService")]
        public IActionResult CreateService () {
            return View ();
        }

        [HttpPost ("/CreateServiceStep1")]
        public IActionResult CreateServiceStep1 (string title, string category, string subcategory, string tags) {
            int ServiceId = gigService.CreateServiceStepOne (title, category, subcategory, tags);
            if (ServiceId == 0) {
                return Redirect ("/CreateService");
            }
            HttpContext.Session.SetInt32 ("serviceId", ServiceId);
            return Redirect ("/CreateService");
        }

        [HttpPost ("/CreateServiceStep2")]
        public IActionResult CreateServiceStep2 (Package pagkage) {
            System.Console.WriteLine(pagkage.Name);
            int? ServiceId = HttpContext.Session.GetInt32 ("serviceId");
            if (gigService.GetServiceByID (ServiceId) != null) {
                bool Check = gigService.CreateServiceStepTwo (pagkage, ServiceId);
                return Redirect ("/CreateService");
            }
            return Redirect ("/CreateService");
        }

        [HttpPost]
        public IActionResult reportGig (int UserId, int ServiceId, string titleReport, string contentReport) {
            if (gigService.reportGig (UserId, ServiceId, titleReport, contentReport)) {
                ViewBag.ReportStatus = true;
            } else {
                ViewBag.ReportStatus = false;
            }
            return View ();
        }
    }
}