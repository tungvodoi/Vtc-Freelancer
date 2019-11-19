using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class UserSellerModel
  {
    public Users users { get; set; }
    public Seller seller { get; set; }

  }
}