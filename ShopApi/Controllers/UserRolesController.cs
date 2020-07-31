using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.BLL.DTO;
using ShopApi.BLL.Services.Interfaces;
using ShopApi.Resource;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    public class UserRolesController : Controller
    {
        private readonly IUserRolesService userRoleService;
        private readonly IMapper mapper;
        public UserRolesController(IUserRolesService userRoleService, IMapper mapper)
        {
            this.mapper = mapper;
            this.userRoleService = userRoleService;
        }
        
        [HttpGet("{id}")]
        public async Task<ResponseData> ListUsersByRoleAsync(int id) 
        {
            var users = await userRoleService.ListUsersByRoleAsync(id);
            var userResource = mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserResource>>(users);
            var result = new ResponseData
            {
                Data = userResource,
                Success = true,
                Message = ""
            };
            return result;
        }

        [HttpPost]
        public async Task<ResponseData> SetUserRole([FromBody] SaveUserRoleResource resource)
        {
            var userResponse = await userRoleService.SetRole(resource.UserId, resource.RoleId);
            var userResource = mapper.Map<UserResource>(userResponse.User);
            var result = new ResponseData
            {
                Success = true,
                Message = "",
                Data = userResource
            };
            return result;
        }

        [HttpDelete]
        public async Task<ResponseData> DeleteUserRole([FromBody] SaveUserRoleResource resource)
        {
            var userResponse = await userRoleService.DeleteRole(resource.UserId, resource.RoleId);
            var userResource = mapper.Map<UserResource>(userResponse.User);
            var result = new ResponseData
            {
                Success = true,
                Message = "",
                Data = userResource
            };
            return result;
        }
    }
}