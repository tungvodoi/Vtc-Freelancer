using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vtc_Freelancer.Models
{
    public class Users
    {
        public int UserId { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string UserName { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Password { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string FullName { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Country { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Address { get; set; }
        [Column(TypeName = "varchar(200)")]
        public int UserLevel { get; set; }
        public DateTime RegisterDate { get; set; }
        public int IsSeller { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Avatar { get; set; }
        public int Status { get; set; }
        public Users() { }

    }
}
