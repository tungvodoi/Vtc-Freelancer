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
        public int ServiceId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        public Order() { }
    }
}