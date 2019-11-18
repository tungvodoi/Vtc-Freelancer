using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string TitleReport { get; set; }
        [Column(TypeName = "text")]
        public string ContentReport { get; set; }
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }


        public Report() { }

    }
}