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
    public class RoleService : IService<RoleDTO, RoleResponse>
    {
        private readonly IRepository<Role> roleRepository;
        private readonly IUnitOfwork unitOfwork;
        private readonly IMapper mapper;
        public RoleService(IRepository<Role> roleRepository, IUnitOfwork unitOfwork, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.unitOfwork = unitOfwork;
            this.mapper = mapper;
        }
        public async Task<RoleResponse> DeleteAsync(int id)
        {
            var existingRole = await roleRepository.FindByIDAsync(id);
            if(existingRole == null)
            {
                return new RoleResponse("Role not found");
            }
            try
            {
                roleRepository.Remove(existingRole);
                await unitOfwork.CompleteAsync();
                return new RoleResponse(existingRole);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Error when deleting role: {ex.Message}");
            }
        }

        public async Task<IEnumerable<RoleDTO>> ListAsync()
        {
            var roles = await roleRepository.ListAsync();
            return mapper.Map<IEnumerable<RoleDTO>>(roles);
        }

        public async Task<RoleResponse> SaveAsync(RoleDTO roleDTO)
        {
            Role role = mapper.Map<Role>(roleDTO);
            try
            {
                await roleRepository.AddASync(role);
                await unitOfwork.CompleteAsync();

                return new RoleResponse(role);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Error when seving the role: {ex.Message}");
            }
        }

        public async Task<RoleResponse> UpdateAsync(int id, RoleDTO roleDTO)
        {
            Role role = mapper.Map<Role>(roleDTO);
            var existingRole = await roleRepository.FindByIDAsync(id);
            if(existingRole == null)
            {
                return new RoleResponse("Role not found");
            }

            existingRole.RoleName = role.RoleName;

            try
            {
                roleRepository.Update(existingRole);
                await unitOfwork.CompleteAsync();
                return new RoleResponse(existingRole);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Error when updating role: {ex.Message}");
            }
        }
    }
}