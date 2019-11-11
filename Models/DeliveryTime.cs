using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class DeliveryTime
  {
    [Column(TypeName = "int")]
    public int PackageId { get; set; }
    [ForeignKey("PackageId")]
    [Column(TypeName = "varchar(50)")]
    public string DeliveryTimes { get; set; }

    public virtual Package Package { get; set; }


    public DeliveryTime() { }

  }
}