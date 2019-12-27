using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vtc_Freelancer.Models;

namespace Vtc_Freelancer.Services
{
    public class OrderService
    {
        private MyDbContext dbContext;
        private AdminService adminService;
        private UserService userService;
        public OrderService(MyDbContext dbContext, AdminService adminService, UserService userService)
        {
            this.dbContext = dbContext;
            this.adminService = adminService;
            this.userService = userService;
        }

        public Package GetPackageByPackageId(int PackageId)
        {
            return dbContext.Package.FirstOrDefault(p => p.PackageId == PackageId);
        }
        public Service GetServiceByServiceId(int? ServiceId)
        {
            return dbContext.Service.FirstOrDefault(x => x.ServiceId == ServiceId);
        }
        public Seller GetUserIdOfSellerBySellerId(int SellerId)
        {
            Seller seller = dbContext.Seller.Include(x => x.User).FirstOrDefault(x => x.SellerId == SellerId);
            return seller;
        }

        public Orders CreateOrder(int UserId, int ServiceId, int PackageId, int Quantity)
        {
            Service service = GetServiceByServiceId(ServiceId);
            Seller seller = userService.GetSellerBySellerID(service.SellerId);
            if (service != null && seller != null && seller.UserId != UserId)
            {
                Orders order = dbContext.Orders.FirstOrDefault(x => x.UserId == UserId && x.ServiceId == ServiceId && x.PackageId == PackageId);
                if (order != null)
                {
                    try
                    {
                        order.WorkStatus = 0;
                        order.Quantity = Quantity;
                        order.OrderCreateTime = DateTime.Now;
                        order.PackageId = PackageId;
                        order.UserId = UserId;
                        order.ServiceId = ServiceId;
                        dbContext.Update(order);
                        dbContext.SaveChanges();
                        return order;
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine("Error : " + ex.Message);
                        return null;
                    }
                }
                else
                {
                    try
                    {
                        Orders newOrder = new Orders();
                        newOrder.WorkStatus = 0;
                        newOrder.Quantity = Quantity;
                        newOrder.OrderCreateTime = DateTime.Now;
                        newOrder.PackageId = PackageId;
                        newOrder.UserId = UserId;
                        newOrder.ServiceId = ServiceId;
                        dbContext.Add(newOrder);
                        dbContext.SaveChanges();
                        return newOrder;
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine("Error : " + ex.Message);
                        return null;
                    }
                }
            }
            return null;
        }

        public Orders GetOrderByPackageId(int PackageId)
        {
            return dbContext.Orders.FirstOrDefault(x => x.PackageId == PackageId);
        }
        public Orders GetOrderByOrderId(int OrderId)
        {
            return dbContext.Orders.FirstOrDefault(x => x.OrderId == OrderId);
        }

        public bool StartOrder(int userId, int OrderId, string ContentRequire, List<Attachments> listFile)
        {
            Orders order = dbContext.Orders.Include(x => x.Service).ThenInclude(x => x.Seller).FirstOrDefault(x => x.OrderId == OrderId);
            try
            {
                if (order != null)
                {
                    order.WorkStatus = 1;
                    order.OrderStartTime = DateTime.Now;
                    dbContext.Update(order);
                    dbContext.SaveChanges();
                    if ((ContentRequire != null && ContentRequire != "") || listFile.Count > 0)
                    {
                        Conversation conver = new Conversation();
                        conver.SenderId = userId;
                        conver.ReceiverId = order.Service.Seller.UserId;
                        conver.Content = ContentRequire;
                        conver.TimeSend = DateTime.Now;
                        conver.IsDeliveredOrIsRevisions = 0;
                        dbContext.Add(conver);
                        dbContext.SaveChanges();
                        foreach (var item in listFile)
                        {
                            Attachments file = new Attachments();
                            file.LinkFile = item.LinkFile;
                            file.FileName = item.FileName;
                            file.ConversationId = conver.ConversationId;
                            dbContext.Add(file);
                            dbContext.SaveChanges();
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
            return false;
        }
        public bool Addnote(int orderId, string noteContent)
        {
            try
            {
                Orders order = dbContext.Orders.FirstOrDefault(x => x.OrderId == orderId);
                if (order != null)
                {
                    order.Note = noteContent;
                    dbContext.Update(order);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool CancelOrder(int orderId, string contentCancelOrder)
        {
            try
            {
                Orders order = dbContext.Orders.FirstOrDefault(x => x.OrderId == orderId);
                if (order != null)
                {
                    order.ReasonCancelOrder = contentCancelOrder;
                    order.WorkStatus = 4;
                    dbContext.Update(order);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool DeliverWord(int userId, int OrderId, string ContentResult, List<Attachments> ListFile)
        {
            try
            {
                Orders order = dbContext.Orders.Include(x => x.Service).FirstOrDefault(x => x.OrderId == OrderId);
                if (order != null)
                {
                    order.WorkStatus = 2;
                    order.OrderDeliveredTime = DateTime.Now;
                    dbContext.Update(order);
                    dbContext.SaveChanges();
                    Conversation conver = new Conversation();
                    if ((ContentResult != null && ContentResult != "") || ListFile.Count > 0)
                    {
                        conver.SenderId = userId;
                        conver.ReceiverId = order.UserId;
                        conver.Content = ContentResult;
                        conver.TimeSend = DateTime.Now;
                        conver.IsDeliveredOrIsRevisions = 1;
                        dbContext.Add(conver);
                        dbContext.SaveChanges();
                        foreach (var item in ListFile)
                        {
                            Attachments file = new Attachments();
                            file.ConversationId = conver.ConversationId;
                            file.LinkFile = item.LinkFile;
                            file.FileName = item.FileName;
                            dbContext.Add(file);
                            dbContext.SaveChanges();
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
        }

        public List<Orders> GetListOrderbyUserId(int? userId)
        {
            List<Orders> listOrder = dbContext.Orders.Where(x => x.UserId == userId).ToList();
            foreach (var item in listOrder)
            {
                item.Service = GetServiceByServiceId(item.ServiceId);
                item.Package = GetPackageByPackageId(item.PackageId);
                item.Service.ListImage = adminService.GetListImageService(item.ServiceId);
            }
            return listOrder;
        }

        public bool ApproveFinalDelivery(int orderId, int userId)
        {
            try
            {
                Orders order = dbContext.Orders.FirstOrDefault(x => x.OrderId == orderId);
                if (order.UserId == userId)
                {
                    order.WorkStatus = 3;
                    dbContext.Update(order);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
        }
        public bool SendOffer(Seller seller, int RequestId, int serviceId, string description)
        {
            try
            {
                Service service = dbContext.Service.FirstOrDefault(x => x.ServiceId == serviceId);
                Request request = dbContext.Request.FirstOrDefault(x => x.RequestId == RequestId);
                if (service != null && request != null)
                {
                    Offer offer = new Offer();
                    offer.Description = description;
                    offer.Amount = 0;
                    offer.Revisions = 0;
                    offer.DeliveryTime = 0;
                    offer.SellerId = seller.SellerId;
                    offer.RequestId = RequestId;
                    offer.ServiceId = serviceId;
                    request.QuantityOffers = request.QuantityOffers += 1;
                    dbContext.Add(offer);
                    dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
        }
    }
}