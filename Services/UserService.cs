using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Vtc_Freelancer.Models;

namespace Vtc_Freelancer.Services
{
  public class UserService
  {
    private MyDbContext dbContext;
    private HashPassword hashPassword;
    public UserService(MyDbContext dbContext, HashPassword hashPassword)
    {
      this.dbContext = dbContext;
      this.hashPassword = hashPassword;
    }
    public Users GetUsersByID(int? ID)
    {
      System.Console.WriteLine(ID);
      Users user = new Users();
      user = dbContext.Users.FirstOrDefault(u => u.UserId == ID);
      return user;
    }
    public bool Register(string username, string email, string password)
    {
      var user = dbContext.Users.FirstOrDefault(x => x.UserName == username);
      if (user != null)
      {
        return false;
      }
      else
      {
        try
        {
          Users User = new Users();
          User.UserName = username;
          User.Email = email;
          User.Password = password;
          User.RegisterDate = DateTime.Now;
          dbContext.Add(User);
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
    // public Users GetUsers()
    public Users Login(string email, string password)
    {
      var character = "@";
      var user = new Users();
      if (email.Contains(character))
      {
        user = dbContext.Users.FirstOrDefault(u => u.Email == email);
        if (user != null)
        {
          if (user.Password == password)
          {
            // System.Console.WriteLine(user.UserName);
            return user;
          }

        }
        return null;
      }
      else
      {
        user = dbContext.Users.FirstOrDefault(u => u.UserName == email);
        if (user != null)
        {
          if (user.Password == password)
          {

            // dbContext.SaveChanges();
            return user;
          }

        }
        return null;
      }

    }
    public bool EditProfile(int Id, string Email, string UserName)
    {
      Users user = new Users();
      user = GetUserByUserId(Id);
      if (user != null)
      {
        user.UserName = UserName;
        user.Email = Email;
        dbContext.Update(user);
        dbContext.SaveChanges();
        return true;
      }

      return false;
    }
    public Users GetUserByUserId(int Id)
    {
      Users users = new Users();
      users = dbContext.Users.FirstOrDefault(u => u.UserId == Id);
      if (users != null)
      {
        return users;
      }
      else
      {
        return null;
      }

    }
    public bool BecomeSeller(Users users)
    {
      try
      {
        Seller seller = new Seller();
        seller.UserId = users.UserId;
        seller.SellerPoint = 0;
        seller.Description = "haha";
        seller.RegisterDateSeller = DateTime.Now;
        dbContext.Add(seller);
        dbContext.SaveChanges();
        return true;
      }
      catch (System.Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
        throw;
      }

    }



  }
}