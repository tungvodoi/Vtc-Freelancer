using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class DeliveryTime
  {
    public int PackageId { get; set; }
    public int DeliveryTimes { get; set; }
    [ForeignKey("PackageId")]
    public virtual Package Package { get; set; }

    public DeliveryTime() { }

  }
}