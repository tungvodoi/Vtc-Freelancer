using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class Rating
  {
    [Column(TypeName = "int")]
    public int RatingId { get; set; }
    [Column(TypeName = "int")]
    public int CountStar { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Content { get; set; }
    [Column(TypeName = "int")]
    public int ServiceId { get; set; }
    [ForeignKey("ServiceId")]
    [Column(TypeName = "int")]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual Service Service { get; set; }

    public Rating() { }

  }
}