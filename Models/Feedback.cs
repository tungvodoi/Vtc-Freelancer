using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class Feedback
  {
    [Column(TypeName = "int")]
    public string FeedbackId { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string Tittle { get; set; }
    [Column(TypeName = "varchar(255)")]
    public int Content { get; set; }
    [Column(TypeName = "int")]
    public int ProjectId { get; set; }
    [Column(TypeName = "int")]
    public int BuyerId { get; set; }
    [Column(TypeName = "int")]
    public int SellerId { get; set; }
    public Feedback() { }

  }
}