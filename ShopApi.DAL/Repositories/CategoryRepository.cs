using ShopApi.DAL.Context;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(ShopContext context) : base(context)
        {
            
        }
    }
}