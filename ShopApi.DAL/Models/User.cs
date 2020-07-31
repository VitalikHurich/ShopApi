using System.Collections.Generic;

namespace ShopApi.DAL.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public IList<Order> Orders { get; set; } = new List<Order>();
        public IList<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}