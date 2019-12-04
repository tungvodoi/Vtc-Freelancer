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
        public bool CreateRequest(int? UserId, string inputRequest, string category, string SubCategory, string inputDeliveredTime, double inputBudget, string urlFile)
        {
            try
            {
                Request req = new Request();
                req.Description = inputRequest;
                req.DeliveredTime = inputDeliveredTime;
                req.Budget = inputBudget;
                req.Category = category;
                req.SubCategory = SubCategory;
                req.LinkFile = urlFile;
                req.TimeCreate = DateTime.Now;
                req.Status = 1;
                req.UserId = UserId;
                dbContext.Add(req);
                dbContext.SaveChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
        }
    }
}