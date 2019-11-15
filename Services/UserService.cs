using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
    }
}