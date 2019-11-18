using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;

namespace Vtc_Freelancer.Controllers
{
    public class GigController : Controller
    {
        private MyDbContext dbContext;
        private GigService gigService;
        public GigController(MyDbContext dbContext, GigService gigService)
        {
            this.dbContext = dbContext;
            this.gigService = gigService;
        }

        public IActionResult reportGig(int UserId, int ServiceId, string titleReport, string contentReport)
        {
            if (gigService.reportGig(UserId, ServiceId, titleReport, contentReport))
            {
                return View();
            }
            else
            {
                return View();
            }
        }
    }
}