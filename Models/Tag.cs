using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string TagName { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        public Tag() { }

    }
}