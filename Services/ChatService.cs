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
            List<Conversation> listConversation = dbContext.Conversation.Where(x => (x.ReceiverId == sellerId && x.SenderId == BuyerId) || (x.ReceiverId == BuyerId && x.SenderId == sellerId)).ToList();
            foreach (var item in listConversation)
            {
                item.ListAttachments = dbContext.Attachments.Where(x => x.ConversationId == item.ConversationId).ToList();
                item.Sender = dbContext.Users.FirstOrDefault(x => x.UserId == item.SenderId);
                item.Receiver = dbContext.Users.FirstOrDefault(x => x.UserId == item.ReceiverId);
            }
            return listConversation;
        }
    }
}