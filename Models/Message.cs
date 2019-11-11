using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class Message
  {
    [Column(TypeName = "int")]
    public string MessageId { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string Content { get; set; }
    [Column(TypeName = "int")]
    public int SenderId { get; set; }
    [Column(TypeName = "int")]
    public int ReceiverId { get; set; }

    public DateTime TimeSend { get; set; }
    [Column(TypeName = "int")]
    public string Status { get; set; }

    public Message() { }

  }
}
