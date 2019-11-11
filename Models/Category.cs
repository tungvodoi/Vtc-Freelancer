using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Column("varchar(200)")]
        public string CategoryName { get; set; }
        public int ParenId { get; set; }
        public Category() { }
    }
}