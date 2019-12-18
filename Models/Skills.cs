using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class Skills
  {
    public int SkillId { get; set; }
    [Column(TypeName = "varchar(200)")]
    public string SkillName { get; set; }

    public int SkillLevel { get; set; }
    public int SellerId { get; set; }
    [ForeignKey("SellerId")]
    public int UserId { get; set; }
    [ForeignKey("UserId")]

    public virtual Seller Seller { get; set; }
    public virtual Users Users { get; set; }
    public Skills() { }

  }
}