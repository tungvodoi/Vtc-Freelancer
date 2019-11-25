using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Package
    {
        public int PackageId { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Title { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }
        public double Price { get; set; }
        public int NumberRevision { get; set; }
        public int DeliveryTime { get; set; }
        public int? ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

        public Package() { }

    }
}