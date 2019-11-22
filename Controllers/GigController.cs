using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;

namespace Vtc_Freelancer.Controllers
{
    // [Authentication]
    public class GigController : Controller
    {
        private GigService gigService;
        private UserService userService;
        public GigController(GigService gigService, UserService userService)
        {
            this.gigService = gigService;
            this.userService = userService;
        }

        [HttpGet("/CreateService/Step1")]
        public IActionResult Step1()
        {
            return View();
        }

        [HttpGet("/CreateService/Step2")]
        public IActionResult Step2()
        {
            StreamReader sr = new StreamReader("./Common/config.json");
            // Read the stream to a string, and write the string to the console.
            String line = sr.ReadToEnd();
            JObject json = JObject.Parse(line);
            sr.Close();
            ViewBag.json = json;
            return View();
        }


        [HttpGet("/CreateService/Step3")]
        public IActionResult Step3()
        {
            return View();
        }


        [HttpGet("/CreateService/Step4")]
        public IActionResult Step4()
        {
            return View();
        }


        // [HttpPost("/CreateService/CreateServiceStep1")]
        // public IActionResult CreateServiceStep1(string title, string category, string subcategory, string tags)
        // {
        //   int? userID = HttpContext.Session.GetInt32("UserId");
        //   int SellerID = userService.GetSellerByUserID(userID).SellerId;
        //   int ServiceId = gigService.CreateServiceStepOne(title, category, subcategory, tags, SellerID);
        //   if (ServiceId == 0)
        //   {
        //     return Redirect("/CreateService/Step1");
        //   }
        //   HttpContext.Session.SetInt32("serviceId", ServiceId);
        //   return Redirect("/CreateService/Step2");
        // }

        [HttpPost("/CreateService/CreateServiceStep2")]
        public IActionResult CreateServiceStep2(string basicTitle, string standardTitle, string premiumTitle, string basicDescription, string standardDescription, string premiumDescription, int basicDelivery, int standardDelivery, int premiumDelivery, int basicRevision, int standardRevision, int premiumRevision, double basicPrice, double standardPrice, double premiumPrice)
        {
            int? ServiceId = HttpContext.Session.GetInt32("serviceId");
            Console.WriteLine(ServiceId);
            Package pacBasic = new Package();
            pacBasic.Name = "BASIC";
            pacBasic.Title = basicTitle;
            pacBasic.Description = basicDescription;
            pacBasic.DeliveryTime = basicDelivery;
            pacBasic.NumberRevision = basicRevision;
            pacBasic.Price = basicPrice;
            pacBasic.ServiceId = ServiceId;
            bool check = gigService.CreateServiceStepTwo(pacBasic);
            if (standardTitle != null && premiumTitle != null && standardDescription != null && premiumDescription != null)
            {
                Package pacStandard = new Package();
                pacStandard.Name = "STANDARD";
                pacStandard.Title = standardTitle;
                pacStandard.Description = standardDescription;
                pacStandard.DeliveryTime = standardDelivery;
                pacStandard.NumberRevision = standardRevision;
                pacStandard.Price = standardPrice;
                pacStandard.ServiceId = ServiceId;
                check = gigService.CreateServiceStepTwo(pacStandard);
                Package pacPremium = new Package();
                pacPremium.Name = "PREMIUM";
                pacPremium.Title = premiumTitle;
                pacPremium.Description = premiumDescription;
                pacPremium.DeliveryTime = premiumDelivery;
                pacPremium.NumberRevision = premiumRevision;
                pacPremium.Price = premiumPrice;
                pacPremium.ServiceId = ServiceId;
                check = gigService.CreateServiceStepTwo(pacPremium);
            }
            if (check)
            {
                return Redirect("/CreateService/Step3");
            }
            return Redirect("/CreateService/Step2");
        }

        [HttpPost("/CreateService/CreateServiceStep3")]
        public IActionResult CreateServiceStep3(string serviceDescription, string question, string reply)
        {
            int? ServiceId = HttpContext.Session.GetInt32("serviceId");
            bool check = gigService.CreateServiceStepThree(ServiceId, serviceDescription, question, reply);
            if (check)
            {
                return Redirect("/CreateService/Step4");
            }
            return Redirect("/CreateService/Step3");
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