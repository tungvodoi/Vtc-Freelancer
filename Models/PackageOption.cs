using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class PackageOption
  {
    [Column(TypeName = "int")]
    public int PackageId { get; set; }
    [ForeignKey("PackageId")]
    [Column(TypeName = "varchar(30)")]
    public string OptionName { get; set; }

    public virtual Package Package { get; set; }


    public PackageOption() { }

  }
}