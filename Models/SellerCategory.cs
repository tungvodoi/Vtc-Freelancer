using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class SellerCategory
  {

    public int SellerId { get; set; }
    // [ForeignKey("SellerId")]
    public int CategoryId { get; set; }
    // [ForeignKey("CategoryId")]

    public virtual Seller Seller { get; set; }

    public virtual Category Category { get; set; }



  }
}