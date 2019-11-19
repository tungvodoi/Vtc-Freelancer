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
            return null;
        }
    }
}