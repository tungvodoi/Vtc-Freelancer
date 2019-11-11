using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class Report
  {
    [Column(TypeName = "int")]
    public int ReportId { get; set; }
    [Column(TypeName = "varchar(40)")]
    public string TittleReport { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string ContentReport { get; set; }
    [Column(TypeName = "int")]
    public int ServiceId { get; set; }
    [ForeignKey("ServiceId")]
    [Column(TypeName = "int")]
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual Service Service { get; set; }


    public Report() { }

  }
}