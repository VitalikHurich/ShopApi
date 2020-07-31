using System.Collections.Generic;
using System.Threading.Tasks;
using ShopApi.BLL.DTO;
using ShopApi.BLL.Response;
using ShopApi.DAL.Models;

namespace ShopApi.BLL.Services.Interfaces
{
    public interface IUserRolesService
    {
        Task<IEnumerable<UserDTO>> ListUsersByRoleAsync(int roleId);
        Task<UserResponse> SetRole(int userId, int roleId);
        Task<UserResponse> DeleteRole(int userId, int roleId);
    }
}