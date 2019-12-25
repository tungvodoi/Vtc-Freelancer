using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public string DeliveredTime { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Category { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string SubCategory { get; set; }
        public double Budget { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }
        [Column(TypeName = "text")]
        public string LinkFile { get; set; }
        public DateTime TimeCreate { get; set; }
        public int QuantityOffers { get; set; }
        public int Status { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
        public Request() { }
    }
}