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
    [Authorize(Roles = "Admin")]
    [Route("api/roles")]
    public class RolesController : Controller
    {
        private readonly IService<RoleDTO, RoleResponse> roleService;
        private readonly IMapper mapper;
        public RolesController(IService<RoleDTO, RoleResponse> roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RoleResource>> GetAllAsync()
        {
            var roles = await roleService.ListAsync();
            var resource = mapper.Map<IEnumerable<RoleDTO>, IEnumerable<RoleResource>>(roles);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveRoleResource resource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var role = mapper.Map<SaveRoleResource, RoleDTO>(resource);
            var result = await roleService.SaveAsync(role);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var role = mapper.Map<SaveRoleResource, RoleDTO>(resource);
            var result = await roleService.UpdateAsync(id, role);

            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await roleService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Role);
        }
    }
}