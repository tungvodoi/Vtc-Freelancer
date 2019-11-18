using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vtc_Freelancer.Models;

namespace Vtc_Freelancer.Services
{
    public class GigService
    {
        private MyDbContext dbContext;
        public GigService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool CreateService(string title, string category,string subcategory,int sellerID)
        {
            // Service service = new Service();
            // service
        }
        public bool reportGig(int UserId, int ServiceId, string titleReport, string contentReport)
        {
            Report report = new Report();
            var user = dbContext.Users.FirstAsync(x => x.UserId == UserId);
            var service = dbContext.Service.FirstAsync(x => x.ServiceId == ServiceId);
            if (user != null && service != null)
            {
                report.TitleReport = titleReport;
                report.ContentReport = contentReport;
                report.ServiceId = ServiceId;
                report.UserId = UserId;
                try
                {
                    dbContext.Add(report);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}