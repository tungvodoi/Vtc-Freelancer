using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
  public class Category
  {
    public int CategoryId { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string CategoryName { get; set; }
    public int ParenId { get; set; }
    public List<SellerCategory> SellerCategorys { get; set; }
    public List<Category> subsCategory = null;
    public Category() { }
  }
}