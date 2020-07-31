using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.BLL.DTO;
using ShopApi.BLL.Services.Interfaces;
using ShopApi.Core.Domain;
using ShopApi.Extensions;
using ShopApi.Resource;

namespace ShopApi.Controllers
{
    [Route("api/goods")]
    public class GoodsController : Controller
    {
        private readonly IGoodService goodService;
        private readonly IMapper mapper;
        public GoodsController(IGoodService goodService, IMapper mapper)
        {
            this.goodService = goodService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<GoodResource>> GetAllAsync([FromBody]GoodParams goodParams)
        {
            var goods = await goodService.ListAsync(goodParams);
            var resource = mapper.Map<PagedList<GoodDTO>, PagedList<GoodResource>>(goods);

            Response.AddPagination(goods.CurrentPage, goods.PageSize, goods.TotalCount, goods.TotalPages);

            return resource;
        }

        // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveGoodResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var good = mapper.Map<SaveGoodResource, GoodDTO>(resource);
            var result = await goodService.SaveAsync(good);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            // var goodResource = mapper.Map<GoodResource>(result.Good);
            return Ok(result.Good);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveGoodResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var good = mapper.Map<SaveGoodResource, GoodDTO>(resource);
            var result = await goodService.UpdateAsync(id, good);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Good);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await goodService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Good);
        }
    }
}