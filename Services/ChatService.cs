using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vtc_Freelancer.Models;

namespace Vtc_Freelancer.Services
{
    public class ChatService
    {
        private MyDbContext dbContext;

        public ChatService(MyDbContext dbContext, HashPassword hashPassword)
        {
            this.dbContext = dbContext;
        }
        public List<Conversation> GetListConversationByUserId(int sellerId, int BuyerId)
        {
            List<Conversation> listConversation = dbContext.Conversation.Include(x => x.ListAttachments).Include(x => x.Sender).Include(x => x.Receiver).Where(x => (x.SenderId == BuyerId || x.SenderId == sellerId) && (x.ReceiverId == BuyerId || x.ReceiverId == sellerId)).ToList();
            return listConversation;
        }
        public bool Chat(int userId, int OrderId, string ContentChat, List<Attachments> ListFile)
        {
            Orders order = dbContext.Orders.Include(x => x.Service).ThenInclude(x => x.Seller).FirstOrDefault(x => x.OrderId == OrderId);
            try
            {
                if (order != null)
                {
                    Conversation conver = new Conversation();
                    if (order.UserId == userId)
                    {
                        conver.SenderId = userId;
                        conver.ReceiverId = order.Service.Seller.UserId;
                    }
                    else
                    {
                        conver.SenderId = order.Service.Seller.UserId;
                        conver.ReceiverId = userId;
                    }
                    if (ContentChat == null || ContentChat == "")
                    {
                        conver.Content = "";
                    }
                    else
                    {
                        conver.Content = ContentChat;
                    }
                    conver.TimeSend = DateTime.Now;
                    conver.IsDeliveredOrIsRevisions = 0;
                    dbContext.Add(conver);
                    dbContext.SaveChanges();
                    foreach (var item in ListFile)
                    {
                        Attachments file = new Attachments();
                        file.LinkFile = item.LinkFile;
                        file.FileName = item.FileName;
                        file.ConversationId = conver.ConversationId;
                        dbContext.Add(file);
                        dbContext.SaveChanges();
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
    }
}