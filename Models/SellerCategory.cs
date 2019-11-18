using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class SellerCategory
    {
        public int SellerCategoryId { get; set; }
        public int SellerId { get; set; }
        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}