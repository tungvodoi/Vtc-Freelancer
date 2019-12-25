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
        public int ConversationDetailId { get; set; }
        [ForeignKey("ConversationDetailId")]
        public virtual ConversationDetail ConversationDetail { get; set; }
        public Attachments() { }
    }
}