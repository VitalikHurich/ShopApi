using System.Threading.Tasks;
using ShopApi.BLL.DTO;

namespace ShopApi.BLL.Services.Interfaces
{
    public interface IAuthService
    {
         Task<UserDTO> Authenticate(string login, string password);
    }
}