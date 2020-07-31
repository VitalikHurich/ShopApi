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
    [Route("api/manufacturers")]
    public class ManufacturersController : Controller
    {
        private readonly IService<ManufacturerDTO, ManufacturerResponse> manufacturerService;
        private readonly IMapper mapper;
        public ManufacturersController(IService<ManufacturerDTO, ManufacturerResponse> manufacturerService, IMapper mapper)
        {
            this.manufacturerService = manufacturerService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ManufacturerResource>> GetAllAsync()
        {
            var manufacturers = await manufacturerService.ListAsync();
            var resource = mapper.Map<IEnumerable<ManufacturerResource>>(manufacturers);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveManufacturerResource resource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var manufacturer = mapper.Map<SaveManufacturerResource, ManufacturerDTO>(resource);
            var result = await manufacturerService.SaveAsync(manufacturer);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Manufacturer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveManufacturerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var manufacturer = mapper.Map<SaveManufacturerResource, ManufacturerDTO>(resource);
            var result = await manufacturerService.UpdateAsync(id, manufacturer);

            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Manufacturer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await manufacturerService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Manufacturer);
        }
    }
}