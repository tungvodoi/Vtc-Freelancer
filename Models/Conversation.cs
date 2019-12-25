using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public ConversationDetail ConversationDetail { get; set; }
        [ForeignKey("BuyerId")]
        public virtual Users User { get; set; }
        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }
        public Conversation() { }

    }
}
