using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vtc_Freelancer.Models;

namespace Vtc_Freelancer.Services
{
    public class RequestService
    {
        private MyDbContext dbContext;
        public RequestService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool CreateRequest(string inputRequest, Category category, string deliveredTime, double budget)
        {
            try
            {
                Request req = new Request();
                req.Description = inputRequest;
                req.DeliveredTime = deliveredTime;
                req.Budget = budget;
                req.Category = category.CategoryName;
                req.SubCategory = category.CategoryName;
                dbContext.Add(req);
                dbContext.SaveChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}