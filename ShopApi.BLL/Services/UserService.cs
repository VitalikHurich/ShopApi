using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ShopApi.BLL.DTO;
using ShopApi.BLL.Response;
using ShopApi.BLL.Services.Interfaces;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.BLL.Services
{
    public class UserService : IService<UserDTO, UserResponse>
    {
        private readonly IRepository<User> userRepository;
        private readonly IUnitOfwork unitOfwork;
        private readonly IMapper mapper;
        public UserService(IRepository<User> userRepository, IUnitOfwork unitOfwork, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.unitOfwork = unitOfwork;
            this.mapper = mapper;
        }
        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingUser = await userRepository.FindByIDAsync(id);
            if(existingUser == null)
            {
                return new UserResponse("User not found");
            }
            try
            {
                userRepository.Remove(existingUser);
                await unitOfwork.CompleteAsync();
                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when deleting user: {ex.Message}");
            }
        }

        public async Task<IEnumerable<UserDTO>> ListAsync()
        {
            var users = await userRepository.ListAsync();
            return mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserResponse> SaveAsync(UserDTO userDTO)
        {
            User user = mapper.Map<User>(userDTO);
            try
            {
                await userRepository.AddASync(user);
                await unitOfwork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when seving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(int id, UserDTO userDTO)
        {
            User user = mapper.Map<User>(userDTO);
            var existingUser = await userRepository.FindByIDAsync(id);
            if(existingUser == null)
            {
                return new UserResponse("User not found");
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Password = user.Password;
            existingUser.UserRoles = user.UserRoles;

            try
            {
                userRepository.Update(existingUser);
                await unitOfwork.CompleteAsync();
                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when updating user: {ex.Message}");
            }
        }
    }
}