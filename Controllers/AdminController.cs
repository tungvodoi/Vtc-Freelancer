using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using System.Linq;

namespace Vtc_Freelancer.Controllers
{
    // [Authentication]
    public class AdminController : Controller
    {
        private MyDbContext dbContext;
        private UserService userService;
        private GigService gigService;
        private AdminService adminService;
        public AdminController(UserService userService, GigService gigService, AdminService adminService, MyDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.gigService = gigService;
            this.adminService = adminService;
        }

        [HttpGet("/Admin/Dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet("/Admin/ManagerServices")]
        public IActionResult ManagerServices(string Search)
        {
            ViewBag.ListServices = adminService.GetListServices(Search);
            return View();
        }

        [HttpGet("/Admin/ManagerOrders")]
        public IActionResult ManagerOrders(string Search)
        {
            ViewBag.ListOrders = adminService.GetListOrders(Search);
            return View();
        }

        [HttpGet("/Admin/ManagerRequests")]
        public IActionResult ManagerRequests(string Search)
        {
            ViewBag.ListRequests = adminService.GetListRequests(Search);
            return View();
        }

        [HttpGet("/Admin/ManagerReports")]
        public IActionResult ManagerReports(string Search)
        {
            ViewBag.ListReport = adminService.GetListReport(Search);
            return View();
        }

        [HttpGet("/Admin/HandleService")]
        public IActionResult HandleService(int ReportId)
        {
            try
            {
                adminService.HandleService(ReportId);
                return Redirect("/Admin/ManagerReports");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return Redirect("/Admin/ManagerUsers");
                throw;
            }

        }

        [HttpGet("/Admin/HandleSeller")]
        public IActionResult HandleSeller(int ReportId)
        {
            try
            {
                adminService.HandleSeller(ReportId);
                return Redirect("/Admin/ManagerReports");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return Redirect("/Admin/ManagerUsers");
                throw;
            }

        }

        [HttpGet("/Admin/ManagerUsers")]
        public IActionResult ManagerUsers(string Search)
        {
            ViewBag.ListUsers = adminService.GetListUsers(Search);
            return View();
        }

        [HttpGet("/Admin/ChangeStatusUser")]

        public IActionResult ChangeStatusUser(int UserId)
        {
            try
            {
                adminService.ChangeStatusUser(UserId);
                return Redirect("/Admin/ManagerUsers");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return Redirect("/Admin/ManagerUsers");
                throw;
            }

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
        public IActionResult GetListCategory()
        {
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();

            if (listcategory != null)
            {
                List<Category> listSubCategory = new List<Category>();
                listSubCategory = adminService.GetListSubCategoryByCategoryParentId(1);

                ViewBag.listcategory = listcategory;
                ViewBag.subcategory = listSubCategory;
                return Redirect("/BecomeSeller");
            }
            return View("Index");
        }

        [HttpPost("/Admin/CreateCategory")]
        public IActionResult CreateCategory(string CategoryName, int ParenId, string SubCategoryName)
        {
            bool category = adminService.CreateCategory(CategoryName, ParenId, SubCategoryName);
            if (category)
            {
                List<Category> listcategory = new List<Category>();
                listcategory = adminService.GetListCategoryBy();
                ViewBag.listcategory = listcategory;
                List<Category> listSubCategory = new List<Category>();
                listSubCategory = adminService.GetListSubCategoryByCategoryParentId(1);
                ViewBag.subcategory = listSubCategory;
                return View();

            }
            return Redirect("/");
        }
        [HttpGet("/Admin/CreateCategory")]
        public IActionResult CreateCategory()
        {
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();

            if (listcategory != null)
            {
                List<Category> listSubCategory = new List<Category>();
                listSubCategory = adminService.GetListSubCategoryByCategoryParentId(1);

                ViewBag.subcategory = listSubCategory;
                ViewBag.listcategory = listcategory;

                return View();
            }
            return View();
        }

        public IActionResult GetListSubCategoryByCategoryParentId(string categoryName)
        {
            Category category = dbContext.Category.FirstOrDefault(u => u.CategoryName == categoryName);
            List<Category> listcategory = new List<Category>();

            listcategory = adminService.GetListSubCategoryByCategoryParentId(category.CategoryId);
            return new JsonResult(listcategory);
        }
        [HttpGet("/Admin/EditCategory")]
        public IActionResult EditCategory(string name)
        {
            ViewBag.CategoryNameEdit = name;
            HttpContext.Session.SetString("CategoryName", name);
            return View();
        }
        [HttpPost("/Admin/EditCategory")]

        public IActionResult EditCategory(Category category)
        {
            var name = HttpContext.Session.GetString("CategoryName");
            if (adminService.EditCategory(category, name))
            {
                return Redirect("/Admin/CreateCategory");
            }
            return View();
        }
    }



}

