using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vtc_Freelancer.Models;

namespace Vtc_Freelancer.Services
{
    public class AdminService
    {
        private MyDbContext dbContext;
        private HashPassword hashPassword;

        public AdminService(MyDbContext dbContext, HashPassword hashPassword)
        {
            this.dbContext = dbContext;
            this.hashPassword = hashPassword;
        }
        public bool CreateCategory(string CategoryName, int ParenId, string SubCategoryName)
        {
            Category ParentCategory = new Category();
            Category SubCategory = new Category();

            var category = dbContext.Category.FirstOrDefault(cat => cat.CategoryName == CategoryName);
            if (category == null)
            {
                ParentCategory.CategoryName = CategoryName;
                dbContext.Add(ParentCategory);
                dbContext.SaveChanges();

                SubCategory.CategoryName = SubCategoryName;
                var category1 = dbContext.Category.FirstOrDefault(cat => cat.CategoryName == CategoryName);
                SubCategory.ParenId = category1.CategoryId;
                dbContext.Add(SubCategory);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                try
                {
                    var Category = dbContext.Category.FirstOrDefault(cat => cat.CategoryName == CategoryName);
                    if (category != null)
                    {

                        SubCategory.CategoryName = SubCategoryName;
                        SubCategory.ParenId = Category.CategoryId;
                        dbContext.Add(SubCategory);
                        dbContext.SaveChanges();
                    }

                    return true;
                }
                catch (System.Exception e)
                {

                    Console.WriteLine(e.Message);
                    return false;

                }
            }
        }
        public List<ImageService> GetListImageService(int idService)
        {
            List<ImageService> ListImageService = dbContext.ImageService.Where(x => x.ServiceId == idService).ToList();

            return ListImageService;
        }
        public List<Service> GetListServices(string Search)
        {
            List<Service> ListServices = dbContext.Service.Include(x => x.Seller).ThenInclude(x => x.User)
            .Where(x => EF.Functions.Like(x.Seller.User.UserName, $"%{Search}%") || EF.Functions.Like(x.Title, $"%{Search}%")
             || EF.Functions.Like(x.Category, $"%{Search}%") || EF.Functions.Like(x.SubCategory, $"%{Search}%") || EF.Functions.Like(x.Description, $"%{Search}%"))
            .OrderByDescending(x => x.TimeCreateService).ToList();
            return ListServices;
        }
        public List<Orders> GetListOrders(string Search)
        {
            List<Orders> ListOrders = dbContext.Order.Include(x => x.Users).Include(x => x.Service)
            .Where(x => EF.Functions.Like(x.Users.UserName, $"%{Search}%") || EF.Functions.Like(x.Service.Title, $"%{Search}%"))
            .OrderByDescending(x => x.OrderTime).ToList();
            return ListOrders;
        }
        public List<Request> GetListRequests(string Search)
        {
            List<Request> ListRequests = dbContext.Request.Include(x => x.Users).Where(x => EF.Functions.Like(x.Users.UserName, $"%{Search}%")
             || EF.Functions.Like(x.DeliveredTime, $"%{Search}%") || EF.Functions.Like(x.Category, $"%{Search}%")
             || EF.Functions.Like(x.SubCategory, $"%{Search}%") || EF.Functions.Like(x.Description, $"%{Search}%"))
            .OrderByDescending(x => x.TimeCreate).ToList();
            return ListRequests;
        }
        public List<Report> GetListReport(string Search)
        {
            List<Report> ListReport = dbContext.Report.Include(x => x.Service).Include(x => x.User)
            .Where(x => EF.Functions.Like(x.User.UserName, $"%{Search}%") || EF.Functions.Like(x.Service.Title, $"%{Search}%") || EF.Functions.Like(x.TitleReport, $"%{Search}%"))
            .OrderByDescending(x => x.TimeCreateReport).ToList();
            foreach (var item in ListReport)
            {
                item.Service = dbContext.Service.FirstOrDefault(x => x.ServiceId == item.ServiceId);
                item.User = dbContext.Users.FirstOrDefault(x => x.UserId == item.UserId);
            }
            return ListReport;
        }
        public List<Users> GetListUsers(string Search)
        {
            List<Users> ListUsers = dbContext.Users.Where(x => EF.Functions.Like(x.UserName, $"%{Search}%") && x.Email != "admin@gmail.com").ToList();
            return ListUsers;
        }

        public bool ChangeStatusUser(int UserId)
        {
            try
            {
                Users user = dbContext.Users.FirstOrDefault(x => x.UserId == UserId);
                if (user.Status != 0)
                {
                    user.Status = 0;
                }
                else
                {
                    user.Status = 1;
                }
                dbContext.Update(user);
                dbContext.SaveChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
        }
        public bool HandleService(int ReportId)
        {
            try
            {
                Report report = dbContext.Report.Include(x => x.Service).FirstOrDefault(x => x.ReportId == ReportId);
                if (report.Status != 1)
                {
                    report.Status = 1;
                }
                if (report.Service.Status != 1)
                {
                    report.Service.Status = 1;
                }
                dbContext.Update(report);
                dbContext.SaveChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
        }
        public bool HandleSeller(int ReportId)
        {
            try
            {
                Report report = dbContext.Report.Include(x => x.Service).ThenInclude(x => x.Seller).ThenInclude(x => x.User).FirstOrDefault(x => x.ReportId == ReportId);
                if (report.Status != 1)
                {
                    report.Status = 1;
                }
                if (report.User.Status != 1)
                {
                    report.User.Status = 1;
                }
                dbContext.Update(report);
                dbContext.SaveChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
        }
        public bool ChangeStatusReport(int ReportId)
        {
            try
            {
                Report report = dbContext.Report.FirstOrDefault(x => x.ReportId == ReportId);
                if (report.Status == 0)
                {
                    report.Status = 1;
                }
                dbContext.Update(report);
                dbContext.SaveChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
        }
        public List<Category> GetListCategoryByParentId(string CategoryName)
        {
            List<Category> listCategory = new List<Category>();
            listCategory = dbContext.Category.FromSql("SELECT * FROM Category WHERE CategoryName = {0}", CategoryName).ToList();
            return listCategory;
        }
        public List<Category> GetListCategoryBy()
        {
            List<Category> listCategory = new List<Category>();
            listCategory = dbContext.Category.Where(c => c.ParenId == 0).ToList();
            return listCategory;
        }
        public List<Category> GetListSubCategoryByCategoryParentId(int id)
        {
            List<Category> listCategory = new List<Category>();
            listCategory = dbContext.Category.FromSql($"SELECT * FROM Category where parenId = {id}").ToList();
            foreach (var item in listCategory)
            {
                Console.WriteLine(item.CategoryName);
            }
            return listCategory;
        }

        public bool EditCategory(Category category, string name)
        {
            var category1 = GetCategoryByCategoryName(name);
            Console.WriteLine(444444444);
            Console.WriteLine(category1.CategoryName);
            if (category1 != null)
            {
                category1.CategoryName = category.CategoryName;
                dbContext.Update(category1);
                dbContext.SaveChanges();
                Console.WriteLine(8888888888888);
                return true;
            }
            return false;
        }
        public Category GetCategoryByCategoryName(string name)
        {
            Category category = new Category();
            category = dbContext.Category.FirstOrDefault(cat => cat.CategoryName == name);
            return category;

        }
    }
}