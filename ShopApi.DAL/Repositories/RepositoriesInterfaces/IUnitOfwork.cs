using System.Threading.Tasks;

namespace ShopApi.DAL.Repositories.RepositoriesInterfaces
{
    public interface IUnitOfwork
    {
         Task CompleteAsync();
    }
}