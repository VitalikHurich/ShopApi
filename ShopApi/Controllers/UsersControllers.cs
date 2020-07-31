using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.BLL.DTO;
using ShopApi.BLL.Response;
using ShopApi.BLL.Services.Interfaces;
using ShopApi.Extensions;
using ShopApi.Resource;

namespace ShopApi.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    [Route("api/users")]
    public class UsersControllers : Controller
    {
        private readonly IService<UserDTO, UserResponse> userService;
        private readonly IUserRolesService userRoleService;
        private readonly IMapper mapper;
        public UsersControllers(IService<UserDTO, UserResponse> userService, IUserRolesService userRoleService, IMapper mapper)
        {
            this.userService = userService;
            this.userRoleService = userRoleService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserResource>> GetAllAsync()
        {
            var users = await userService.ListAsync();
            var resource = mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserResource>>(users);
            return resource;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var user = mapper.Map<SaveUserResource, UserDTO>(resource); 
            var result = await userService.SaveAsync(user);
            await userRoleService.SetRole(user.UserId, 3); //set default role User

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.User);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = mapper.Map<SaveUserResource, UserDTO>(resource);
            var result = await userService.UpdateAsync(id, user);

            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.User);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await userService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.User);
        }
    }
}