using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class Service
  {
    public int ServiceId { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Title { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Category { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string SubCategory { get; set; }
    [Column(TypeName = "text")]
    public string Description { get; set; }
    public DateTime TimeCreateService { get; set; }
    public int Status { get; set; }
    public int SellerId { get; set; }
    public List<ImageService> ListImage { get; set; }
    [ForeignKey("SellerId")]
    public virtual Seller Seller { get; set; }
    public Service() { }

  }
}