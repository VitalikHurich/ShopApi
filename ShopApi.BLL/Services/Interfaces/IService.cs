using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.BLL.Services.Interfaces
{
    public interface IService<T, TResponse>
    {
        Task<IEnumerable<T>> ListAsync();
        Task<TResponse> SaveAsync(T entity);
        Task<TResponse> UpdateAsync(int id, T entity);
        Task<TResponse> DeleteAsync(int id);
    }
}