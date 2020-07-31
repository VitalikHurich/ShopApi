using System.Collections.Generic;

namespace ShopApi.DAL.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}