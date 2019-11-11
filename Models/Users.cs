using System;

namespace Vtc_Freelancer.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string Addres { get; set; }
        public int UserLevel { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsSeller { get; set; }
        public string Avatar { get; set; }
        public bool Status { get; set; }
        public Users() { }

    }
}
