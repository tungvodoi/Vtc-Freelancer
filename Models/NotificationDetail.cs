using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class NotificationDetail
    {
        public int NotificationDetailId { get; set; }
        public int NotificationId { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }

        [ForeignKey("NotificationId")]
        public virtual List<Notification> ListNotification { get; set; }
        public NotificationDetail() { }

    }
}