using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int CountStar { get; set; }
        [Column(TypeName = "text")]
        public string Content { get; set; }
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
        public Rating() { }

    }
}