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
    
  }
}