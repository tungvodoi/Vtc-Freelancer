using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        [ForeignKey("BuyerId")]
        public virtual Users User { get; set; }
        public Conversation() { }

    }
}
