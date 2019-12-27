using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Offer
    {
        public int OfferId { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }
        public Double Amount { get; set; }
        public int Revisions { get; set; }
        public int DeliveryTime { get; set; }
        public int SellerId { get; set; }
        public int RequestId { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }
        [ForeignKey("RequestId")]
        public virtual Request Request { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        public Offer() { }
        public Users users = null;
    }
}