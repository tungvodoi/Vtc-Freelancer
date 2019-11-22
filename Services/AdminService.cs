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
    public List<Service> GetListServices(string Search)
    {
      List<Service> ListServices = dbContext.Service.FromSql(@"select s.ServiceId, s.Title, s.Category, s.SubCategory, s.Description, s.TimeCreateService, s.Status, s.SellerId, u.Username 
            from Service s inner join seller se on s.sellerid = se.sellerid inner join users u on se.userid = u.userid
            where s.Title like '%" + Search + "%' or s.Category like '%" + Search + "%' or s.SubCategory like '%" + Search + "%' or s.Description like '%" + Search + "%' or u.Username like '%" + Search + "%' order by TimeCreateService desc").ToList();
      foreach (var item in ListServices)
      {
        item.Seller = dbContext.Seller.FirstOrDefault(x => x.SellerId == item.SellerId);
        item.Seller.User = dbContext.Users.FirstOrDefault(x => x.UserId == item.Seller.UserId);
      }
      return ListServices;
    }
    public List<Report> GetListReport(string Search)
    {
      List<Report> ListReport = dbContext.Report.FromSql(@"select re.reportId, re.TitleReport, re.ContentReport, re.TimeCreateReport, re.Status, re.ServiceId, re.UserId, u.Username, s.Title 
            from report re inner join users u on re.userid = u.userid inner join service s on re.serviceid = s.serviceid where u.Username like '%" + Search + "%' or s.Title like '%" + Search + "%' or re.TitleReport like '%" + Search + "%' order by TimeCreateReport desc").ToList();
      foreach (var item in ListReport)
      {
        item.Service = dbContext.Service.FirstOrDefault(x => x.ServiceId == item.ServiceId);
        item.User = dbContext.Users.FirstOrDefault(x => x.UserId == item.UserId);
      }
      return ListReport;
    }
    public List<Users> GetListUsers(string Search)
    {
      List<Users> ListUsers = dbContext.Users.FromSql("select * from Users where Username like '%" + Search + "%' or Email like '%" + Search + "%'").ToList();
      return ListUsers;
    }
    public bool ChangeStatusUser(int UserId)
    {
      try
      {
        Users user = dbContext.Users.FirstOrDefault(x => x.UserId == UserId);
        if (user.Status == 1)
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
      listCategory = dbContext.Category.FromSql("SELECT * FROM Category where parenId = 0").ToList();
      foreach (var item in listCategory)
      {
        Console.WriteLine(item.CategoryName);
      }
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

    public bool EditCategory(Category category)
    {
      var category1 = dbContext.Category.FirstOrDefault(cat => cat.CategoryName == category.CategoryName);
      if (category != null)
      {
        category1.CategoryName = category.CategoryName;
        dbContext.Update(category1);
        dbContext.SaveChanges();
        return true;
      }

      return false;
    }
  }
}