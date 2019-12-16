using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net;
namespace Vtc_Freelancer.ActionFilter
{
    public class Authentication : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetInt32("UserId") == null)
            {
                var url = context.HttpContext.Request.GetDisplayUrl();
                var myEncodedString = System.Net.WebUtility.UrlEncode(url);
                Console.WriteLine(1);
                Console.WriteLine(url);
                Console.WriteLine(myEncodedString);
                Console.WriteLine(2);


                var controller = (Controller)context.Controller;
                context.Result = controller.Redirect("/login?returnUrl=" + myEncodedString);
            }
        }
    }
}