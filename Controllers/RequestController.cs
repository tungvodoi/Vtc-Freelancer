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
    public class RequestController : Controller
    {
        private MyDbContext dbContext;
        private RequestService requestService;
        private AdminService adminService;
        public RequestController(MyDbContext dbContext, RequestService requestService, AdminService adminService)
        {

            this.dbContext = dbContext;
            this.requestService = requestService;
            this.adminService = adminService;
        }

        [HttpGet("/manager_request")]
        public IActionResult FormInputRequest()
        {
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();

            if (listcategory != null)
            {
                List<Category> listSubCategory = new List<Category>();
                listSubCategory = adminService.GetListSubCategoryByCategoryParentId(1);

                ViewBag.subcategory = listSubCategory;
                ViewBag.listcategory = listcategory;

                return View("request");
            }
            return View();
        }

        [HttpPost("/manager_request")]
        public IActionResult CreateRequest(string inputRequest, Category category, string deliveredTime, double budget)
        {
            var categoryName = dbContext.Category.FirstOrDefault(x => x.CategoryName == category.CategoryName);
            if (requestService.CreateRequest(inputRequest, category, deliveredTime, budget))
            {
                return Redirect("/");
            }
            else
            {
                return Redirect("/manager_request");
            }
        }
    }
}