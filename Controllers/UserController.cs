using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vtc_Freelancer.Models;
using Vtc_Freelancer.Services;

namespace Vtc_Freelancer.Controllers
{
    public class UserController : Controller
    {
        private MyDbContext dbContext;
        private HashPassword hashPassword;
        private UserService userService;
        public UserController(MyDbContext dbContext, HashPassword hashPassword, UserService userService)
        {
            this.dbContext = dbContext;
            this.hashPassword = hashPassword;
            this.userService = userService;
            dbContext.Database.EnsureCreated();
        }
        [HttpPost("/Register")]
        public IActionResult Register(string username, string email, string password)
        {
            if (userService.Register(username, email, password))
            {
                ViewBag.Noti = true;
            }
            return Redirect("/Login");
        }
        [HttpGet("/Register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}