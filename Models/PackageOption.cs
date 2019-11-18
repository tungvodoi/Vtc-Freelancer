using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class PackageOption
    {
        public int PackageId { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string OptionName { get; set; }
        public int OptionStatus { get; set; }
        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }


        public PackageOption() { }

    }
}