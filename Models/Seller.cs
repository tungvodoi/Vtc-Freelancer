using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class Seller
  {
    public int SellerId { get; set; }
    public int SellerPoint { get; set; }
    [Column(TypeName = "text")]
    public string Description { get; set; }
    public DateTime RegisterDateSeller { get; set; }
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual Users User { get; set; }
    public Seller() { }

  }
}