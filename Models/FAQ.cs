using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class FAQ
  {
    [Column(TypeName = "int")]
    public int FAQId { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Question { get; set; }
    [Column(TypeName = "varchar(55)")]
    public string Reply { get; set; }
    [Column(TypeName = "int")]
    public int ServiceId { get; set; }
    [ForeignKey("ServiceId")]
    public virtual Service Service { get; set; }


    public FAQ() { }

  }
}