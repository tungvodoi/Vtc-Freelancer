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
        public List<Conversation> GetListConversationBySellerIdAndBuyerId(int sellerId, int BuyerId)
        {
            return dbContext.Conversation.Where(x => x.SellerId == sellerId && x.BuyerId == BuyerId).ToList();
        }

        public ConversationDetail GetConversationDetailByConversationId(int ConversationId)
        {
            ConversationDetail converDetail = dbContext.ConversationDetail.FirstOrDefault(x => x.ConversationId == ConversationId);
            converDetail.ListAttachments = dbContext.Attachments.Where(x => x.ConversationDetailId == converDetail.ConversationDetailId).ToList();
            return converDetail;
        }
    }
}