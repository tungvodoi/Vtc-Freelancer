using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class ImageService
  {
    public int ImageServiceID { get; set; }
    [Column(TypeName = "text")]
    public string Image { get; set; }
    public int? ServiceId { get; set; }
    [ForeignKey("ServiceId")]
    public virtual Service Service { get; set; }
    public ImageService() { }

  }
}