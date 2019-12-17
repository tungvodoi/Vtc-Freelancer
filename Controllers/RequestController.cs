using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Vtc_Freelancer.ActionFilter;

namespace Vtc_Freelancer.Controllers
{
    public class RequestController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private MyDbContext dbContext;
        private RequestService requestService;
        private AdminService adminService;
        private UserService userService;
        public RequestController(MyDbContext dbContext, RequestService requestService, AdminService adminService, UserService userService, IHostingEnvironment IHostingEnvironment)
        {
            this.userService = userService;
            this.dbContext = dbContext;
            this.requestService = requestService;
            this.adminService = adminService;
            this._environment = IHostingEnvironment;
        }
        [Authentication]
        [HttpGet("/manager_request")]
        public IActionResult FormInputRequest()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();
            foreach (var item in listcategory)
            {
                item.subsCategory = adminService.GetListSubCategoryByParentId(item.CategoryId);
            }
            if (listcategory != null)
            {
                ViewBag.listcategory = listcategory;
                return View("Request");
            }

            return View("Request");
        }

        [HttpGet("/view_requests")]
        public IActionResult ViewRequests()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            List<Category> listcategory = new List<Category>();
            listcategory = adminService.GetListCategoryBy();
            foreach (var item in listcategory)
            {
                item.subsCategory = adminService.GetListSubCategoryByParentId(item.CategoryId);
            }
            if (listcategory != null)
            {
                ViewBag.listcategory = listcategory;
                return View();
            }
            Users users = userService.GetUserByUsername(HttpContext.Session.GetString("UserName"));
            return View("Request/ViewRequests");
        }

        [HttpPost("/manager_request")]
        public IActionResult CreateRequest(string inputRequest, string category, string SubCategory, string inputDeliveredTime, double inputBudget, string name)
        {
            string urlFile = "";
            var UserId = HttpContext.Session.GetInt32("UserId");
            var newFileName = string.Empty;
            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;
                string PathDB = string.Empty;
                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "FileRequest/") + $@"\{newFileName}";

                        // if you want to store path of folder in database
                        urlFile = "FileRequest/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
            }
            if (requestService.CreateRequest(UserId, inputRequest, category, SubCategory, inputDeliveredTime, inputBudget, urlFile))
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