using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class FAQ
    {
        public int FAQId { get; set; }
        [Column(TypeName = "text")]
        public string Question { get; set; }
        [Column(TypeName = "text")]
        public string Reply { get; set; }
        public int? ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }


        public FAQ() { }

    }
}