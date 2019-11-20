using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Http;
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

          // ParentCategory.CategoryName = CategoryName;
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
    public List<Category> GetListCategoryByParentId(string CategoryName)
    {
      List<Category> listCategory = new List<Category>();
      listCategory = dbContext.Category.FromSql("SELECT * FROM Category WHERE CategoryName = {0}", CategoryName).ToList();

      return listCategory;
    }
    public List<Category> GetListCategoryBy()
    {
      List<Category> listCategory = new List<Category>();
      listCategory = dbContext.Category.FromSql("SELECT * FROM Category").ToList();
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