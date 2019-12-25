using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class ConversationDetail
    {
        public int ConversationDetailId { get; set; }
        public int ConversationId { get; set; }
        public int SenderId { get; set; }
        [Column(TypeName = "text")]
        public string Content { get; set; }
        public List<Attachments> ListAttachments { get; set; }
        public DateTime TimeSend { get; set; }
        public int Status { get; set; }
        [ForeignKey("ConversationId")]
        public virtual Conversation Conversation { get; set; }
        public ConversationDetail() { }
    }
}