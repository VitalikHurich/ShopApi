using System.Threading.Tasks;
using ShopApi.BLL.DTO;
using ShopApi.BLL.Response;
using ShopApi.Core.Domain;
using ShopApi.DAL.Models;

namespace ShopApi.BLL.Services.Interfaces
{
    public interface IGoodService
    {
        Task<PagedList<GoodDTO>> ListAsync(GoodParams goodParams);
        Task<GoodResponse> SaveAsync(GoodDTO goodDTO);
        Task<GoodResponse> UpdateAsync(int id, GoodDTO goodDTO);
        Task<GoodResponse> DeleteAsync(int id);
    }
}