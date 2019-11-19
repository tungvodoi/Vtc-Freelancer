using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vtc_Freelancer.Models;

namespace Vtc_Freelancer.Services
{
    public class AdminService
    {
        private MyDbContext dbContext;
        public AdminService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Service> GetListServicesHadActive()
        {
            List<Service> ListServiceHadActive = dbContext.Service.FromSql("select * from Service where status = 1 order by TimeCreateService desc").ToList();
            return ListServiceHadActive;
        }

        public List<Service> GetListServicesInactive()
        {
            List<Service> ListServiceInactive = dbContext.Service.FromSql("select * from Service where status = 0 order by TimeCreateService desc").ToList();
            return ListServiceInactive;
        }
        public List<Users> GetListUsers(string Username)
        {
            List<Users> ListUsers = dbContext.Users.FromSql("select * from Users where Username like '%" + Username + "%'").ToList();
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
    }
}