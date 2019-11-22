using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int WorkStatus { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderTime { get; set; }
        public int UserId { get; set; }
        public int PackageId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }
        public Order() { }
    }
}