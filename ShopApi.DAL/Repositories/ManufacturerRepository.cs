using ShopApi.DAL.Context;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.DAL.Repositories
{
    public class ManufacturerRepository : GenericRepository<Manufacturer>
    {
        public ManufacturerRepository(ShopContext context) : base(context)
        {
            
        }
    }
}