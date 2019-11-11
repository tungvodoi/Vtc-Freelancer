using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Request
    {
        public int RequestId { get; set; }

        public string Delivered { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public double Budget { get; set; }
        public string Description { get; set; }
        public DateTime TimeCreate { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
        public Request() { }
    }
}