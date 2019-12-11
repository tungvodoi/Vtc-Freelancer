using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vtc_Freelancer.Models;

namespace Vtc_Freelancer.Services
{
    public class OrderService
    {
        private MyDbContext dbContext;
        public OrderService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Package GetPackageByPackageId(int PackageId)
        {
            return dbContext.Package.FirstOrDefault(p => p.PackageId == PackageId);
        }
        public Service GetServiceByServiceId(int? ServiceId)
        {
            return dbContext.Service.FirstOrDefault(x => x.ServiceId == ServiceId);
        }
    }
}