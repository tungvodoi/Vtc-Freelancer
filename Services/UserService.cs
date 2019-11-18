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
      var user = dbContext.Users.FirstOrDefault(x => x.UserName == username || x.Email == email);
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
          return false;
        }
      }
    }
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
            return user;
          }

        }
        return null;
      }

    }
    public bool EditProfile(int Id, string Email, string UserName)
    {
      Users user = new Users();
      user = GetUsersByID(Id);
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
  }
}