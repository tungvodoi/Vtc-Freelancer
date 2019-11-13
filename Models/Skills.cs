using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Skills
    {
        public int SkillId { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string SkillName { get; set; }

        public int Level { get; set; }
        public int SellerId { get; set; }
        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }
        public Skills() { }

    }
}