using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Attachments
    {
        public int AttachmentsId { get; set; }
        public string FileName { get; set; }
        [Column(TypeName = "text")]
        public string LinkFile { get; set; }
        public int ConversationId { get; set; }
        [ForeignKey("ConversationId")]
        public virtual Conversation Conversation { get; set; }
        public Attachments() { }
    }
}