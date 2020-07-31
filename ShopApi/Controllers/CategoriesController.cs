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
    // [Authorize(Roles = "Admin")]
    [AllowAnonymous]
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        private readonly IService<CategoryDTO, CategoryResponse> categoryService;
        private readonly IMapper mapper;
        public CategoriesController(IService<CategoryDTO, CategoryResponse> categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        // [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<CategoryResource>> GetAllAsync()
        {
            var categories = await categoryService.ListAsync();
            var resource = mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryResource>>(categories);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResource resource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var category = mapper.Map<SaveCategoryResource, CategoryDTO>(resource);
            var result = await categoryService.SaveAsync(category);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            // var categoryResource = mapper.Map<CategoryResource>(result.Category);
            return Ok(result.Category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = mapper.Map<SaveCategoryResource, CategoryDTO>(resource);
            var result = await categoryService.UpdateAsync(id, category);

            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await categoryService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Category);
        }
    }
}