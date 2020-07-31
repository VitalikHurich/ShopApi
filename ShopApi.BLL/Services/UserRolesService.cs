using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShopApi.BLL.DTO;
using ShopApi.BLL.Response;
using ShopApi.BLL.Services.Interfaces;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.BLL.Services
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Role> roleRepository;
        private readonly IUnitOfwork unitOfWork;
        private readonly IMapper mapper;
        public UserRolesService(IRepository<User> userRepository, IRepository<Role> roleRepository, IUnitOfwork unitOfWork, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<UserResponse> DeleteRole(int userId, int roleId)
        {
            try
            {
                var user = await userRepository.FindByIDAsync(userId);
                user.UserRoles.Remove(user.UserRoles.FirstOrDefault(x => x.RoleId == roleId));
                await unitOfWork.CompleteAsync();
                user = await userRepository.FindByIDAsync(userId);
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when deleting role: {ex.Message}");
            }
        }

        public async Task<IEnumerable<UserDTO>> ListUsersByRoleAsync(int roleId)
        {
            var users = await userRepository.ListAsync();
            var usersInRole = users.Where(x => x.UserRoles.Contains(x.UserRoles.SingleOrDefault(y => y.RoleId == roleId)));
            return mapper.Map<IEnumerable<UserDTO>>(usersInRole);

        }

        public async Task<UserResponse> SetRole(int userId, int roleId)
        {
            try
            {
                var user = await userRepository.FindByIDAsync(userId);
                user.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
                await unitOfWork.CompleteAsync();
                await roleRepository.ListAsync();
                user = await userRepository.FindByIDAsync(userId);
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when setting role: {ex.Message}");
            }

        }
    }
}