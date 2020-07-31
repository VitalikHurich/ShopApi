using System.Threading.Tasks;
using ShopApi.DAL.Context;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.DAL.Repositories
{
    public class UnitOfWork : IUnitOfwork
    {
        private readonly ShopContext context;
        public UnitOfWork(ShopContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}