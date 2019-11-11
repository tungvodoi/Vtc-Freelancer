using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class Notification
  {
    [Column(TypeName = "int")]
    public string NotificationId { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string Content { get; set; }
    [Column(TypeName = "int")]
    public int Status { get; set; }
    [Column(TypeName = "int")]
    public int UserId { get; set; }

    public DateTime TimeSend { get; set; }



    public Notification() { }

  }
}