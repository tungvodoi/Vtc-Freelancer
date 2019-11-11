using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class Package
  {
    [Column(TypeName = "int")]
    public int PackageId { get; set; }
    [Column(TypeName = "varchar(55")]
    public string Name { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Description { get; set; }
    [Column(TypeName = "double")]
    public double Price { get; set; }
    [Column(TypeName = "int")]
    public int NumberRevision { get; set; }
    [Column(TypeName = "int")]
    public int ServiceId { get; set; }
    [ForeignKey("ServiceId")]
    public virtual Service Service { get; set; }

    public Package() { }

  }
}