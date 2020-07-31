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
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly IService<OrderDTO, OrderResponse> orderService;
        private readonly IMapper mapper;
        public OrdersController(IService<OrderDTO, OrderResponse> orderService, 
                                IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<IEnumerable<OrderResource>> GetAllAsync()
        {
            var orders = await orderService.ListAsync();
            var resource = mapper.Map<IEnumerable<OrderDTO>, IEnumerable<OrderResource>>(orders);

            return resource;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveOrderResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var order = mapper.Map<SaveOrderResource, OrderDTO>(resource);
            var result = await orderService.SaveAsync(order);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Order);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveOrderResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var order = mapper.Map<SaveOrderResource, OrderDTO>(resource);
            var result = await orderService.UpdateAsync(id, order);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Order);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await orderService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Order);
        }
    }
}