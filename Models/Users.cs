using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Users
    {
        public int UserId { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string UserName { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Password { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string FullName { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Country { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Address { get; set; }
        public int UserLevel { get; set; }
        public DateTime RegisterDate { get; set; }
        public int IsSeller { get; set; }
        [Column(TypeName = "text")]
        public string Avatar { get; set; }
        public int Status { get; set; }
        public Users() { }

    }
}
