using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Column("varchar(200)")]
        public int WorkStatus { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderTime { get; set; }
        public int UserId { get; set; }
        public int SellerId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
        [ForeignKey("SellerId")]
        public virtual Seller Seller { get; set; }
        public Order() { }
    }
}