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

        public bool CreateOrder(int UserId, int ServiceId, int PackageId, int Quantity)
        {
            Orders order = dbContext.Orders.FirstOrDefault(x => x.UserId == UserId && x.ServiceId == ServiceId && x.PackageId == PackageId);
            if (order != null)
            {
                try
                {
                    order.WorkStatus = 0;
                    order.Quantity = Quantity;
                    order.OrderTime = DateTime.Now;
                    order.PackageId = PackageId;
                    order.UserId = UserId;
                    order.ServiceId = ServiceId;
                    dbContext.Update(order);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Error : " + ex.Message);
                    return false;
                }
            }
            else
            {
                try
                {
                    Orders newOrder = new Orders();
                    newOrder.WorkStatus = 0;
                    newOrder.Quantity = Quantity;
                    newOrder.OrderTime = DateTime.Now;
                    newOrder.PackageId = PackageId;
                    newOrder.UserId = UserId;
                    newOrder.ServiceId = ServiceId;
                    dbContext.Add(newOrder);
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

        public Orders GetOrderByPackageId(int PackageId)
        {
            return dbContext.Orders.FirstOrDefault(x => x.PackageId == PackageId);
        }
        public Orders GetOrderByOrderId(int OrderId)
        {
            return dbContext.Orders.FirstOrDefault(x => x.OrderId == OrderId);
        }

        public bool StartOrder(int OrderId, string ContentRequire, string urlFile)
        {
            Orders order = dbContext.Orders.FirstOrDefault(x => x.OrderId == OrderId);
            try
            {
                if (order != null)
                {
                    order.WorkStatus = 1;
                    order.ContentRequire = ContentRequire;
                    order.File = urlFile;
                    dbContext.Update(order);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
            return false;
        }
    }
}