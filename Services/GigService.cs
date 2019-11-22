using System;
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
        public int CreateServiceStepOne(string title, string category, string subcategory, string tags, int sellerId)
        {
            Service service = new Service();
            try
            {
                service.Title = title;
                service.Category = category;
                service.SubCategory = subcategory;
                service.TimeCreateService = System.DateTime.Now;
                service.Status = 1;
                service.SellerId = sellerId;
                dbContext.Add(service);
                dbContext.SaveChanges();
                string[] ListTags = tags.Split(',');
                foreach (var item in ListTags)
                {
                    if (item != "")
                    {
                        Tag tag = new Tag();
                        tag.TagName = item;
                        tag.ServiceId = service.ServiceId;
                        dbContext.Add(tag);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
            return service.ServiceId;
        }
        public bool CreateServiceStepTwo(Package package)
        {
            try
            {
                dbContext.Add(package);
                dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
        public bool CreateServiceStepThree(int? serviceID, string descripton, string question, string reply)
        {
            try
            {
                Service ser = new Service();
                ser = dbContext.Service.FirstOrDefault(ser => ser.ServiceId == serviceID);
                ser.Description = descripton;
                dbContext.SaveChanges();
                FAQ faq = new FAQ();
                faq.Question = question;
                faq.Reply = reply;
                faq.ServiceId = serviceID;
                dbContext.Add(faq);
                dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
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
        public Service GetServiceByID(int? ID)
        {
            return dbContext.Service.FirstOrDefault(x => x.ServiceId == ID);
        }
        public Package GetPackageByID(int ID)
        {
            return dbContext.Package.FirstOrDefault(p => p.PackageId == ID);
        }



    }
}