using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.DAL.Repositories.RepositoriesInterfaces
{
    public interface IRepository<T> where T : class
    {
         Task<IEnumerable<T>> ListAsync();
         Task AddASync(T entity);
         void Update(T entity);
         Task<T> FindByIDAsync(int id);
         void Remove(T entity);
    }
}