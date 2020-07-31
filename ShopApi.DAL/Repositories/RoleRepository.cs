using ShopApi.DAL.Context;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.DAL.Repositories
{
    public class RoleRepository : GenericRepository<Role>
    {
        public RoleRepository(ShopContext context) : base(context)
        {
            
        }
    }
}