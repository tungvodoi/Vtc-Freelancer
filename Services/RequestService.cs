using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vtc_Freelancer.Models;

namespace Vtc_Freelancer.Services
{
    public class RequestService
    {
        private MyDbContext dbContext;
        public RequestService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool CreateRequest(string inputRequest, Category category, Skills skill)
        {
            return true;
        }
    }
}