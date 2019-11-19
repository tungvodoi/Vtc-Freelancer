using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Vtc_Freelancer.Controllers;

namespace Vtc_Freelancer.ActionFilter
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // if (context.HttpContext.Session.GetInt32("UserId") == null)
            // {
            //     var controller = (Controller)context.Controller;
            // context.Result = controller.Redirect("/Login");
            // }
        }
    }
}