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
        public string Addres { get; set; }
        [Column(TypeName = "varchar(200)")]
        public int UserLevel { get; set; }
        public DateTime RegisterDate { get; set; }
        [Column(TypeName = "int")]
        public bool IsSeller { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Avatar { get; set; }
        [Column(TypeName = "int")]
        public bool Status { get; set; }
        public Users() { }

    }
}
