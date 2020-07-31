using System.Threading.Tasks;
using ShopApi.Core.Domain;
using ShopApi.DAL.Models;

namespace ShopApi.DAL.Repositories.RepositoriesInterfaces
{
    public interface IGoodRepository
    {
         Task<PagedList<Good>> ListAsync(GoodParams goodParams);
         Task AddASync(Good good);
         void Update(Good good);
         Task<Good> FindByIDAsync(int id);
         void Remove(Good good);
    }
}