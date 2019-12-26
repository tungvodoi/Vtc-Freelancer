using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        [Column(TypeName = "text")]
        public string Content { get; set; }
        public List<Attachments> ListAttachments { get; set; }
        public DateTime TimeSend { get; set; }
        public int Status { get; set; }
        public int IsDeliveredOrIsRevisions { get; set; }
        [ForeignKey("SenderId")]
        public virtual Users Sender { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual Users Receiver { get; set; }
        public Conversation() { }
    }
}