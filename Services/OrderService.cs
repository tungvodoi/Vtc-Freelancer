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
    public Package GetPackageByID(int ID)
    {
      return dbContext.Package.FirstOrDefault(p => p.PackageId == ID);
    }
    public Service GetServiceByID(int? ID)
    {
      return dbContext.Service.FirstOrDefault(x => x.ServiceId == ID);
    }

  }
}