using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class Tag
  {
    [Column(TypeName = "int")]
    public int TagId { get; set; }
    [Column(TypeName = "varchar(30)")]
    public string TagName { get; set; }
    [Column(TypeName = "int")]
    public int ServiceId { get; set; }
    [ForeignKey("ServiceId")]
    public virtual Service Service { get; set; }

    public Tag() { }

  }
}