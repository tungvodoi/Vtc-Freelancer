using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class Service
  {
    [Column(TypeName = "int")]
    public int ServiceId { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Title { get; set; }
    [Column(TypeName = "varchar(30)")]
    public string Category { get; set; }
    [Column(TypeName = "varchar(30)")]
    public string SubCategory { get; set; }
    [Column(TypeName = "varchar(55)")]
    public string Description { get; set; }
    [Column(TypeName = "int")]
    public int Status { get; set; }
    [Column(TypeName = "int")]
    public int SellerId { get; set; }
    [Column(TypeName = "int")]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual Seller Seller { get; set; }
    public Service() { }

  }
}