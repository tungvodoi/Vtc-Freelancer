using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int WorkStatus { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderCreateTime { get; set; }
        public DateTime OrderStartTime { get; set; }
        public DateTime OrderDeliveredTime { get; set; }
        public int PackageId { get; set; }
        public int UserId { get; set; }
        public string ContentRequire { get; set; }
        [Column(TypeName = "text")]
        public string FileRequire { get; set; }
        [Column(TypeName = "text")]
        public string FileResult { get; set; }
        [Column(TypeName = "text")]
        public string ContentReply { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }
        public Orders() { }
    }
}