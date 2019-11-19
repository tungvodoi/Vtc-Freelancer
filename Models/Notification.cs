using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }

        public DateTime TimeSend { get; set; }
        public Notification() { }

    }
}