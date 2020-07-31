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
    [Route("api/orderdetails")]
    public class OrderDetailsController : Controller
    {
        private readonly IService<OrderDetailDTO, OrderDetailResponse> orderDetailService;
        private readonly IMapper mapper;
        public OrderDetailsController(IService<OrderDetailDTO, OrderDetailResponse> orderDetailService, 
                                        IMapper mapper)
        {
            this.orderDetailService = orderDetailService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<IEnumerable<OrderDetailResource>> GetAllAsync()
        {
            var orderDetails = await orderDetailService.ListAsync();
            var resource = mapper.Map<IEnumerable<OrderDetailDTO>, IEnumerable<OrderDetailResource>>(orderDetails);

            return resource;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveOrderDetailResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var orderDetail = mapper.Map<SaveOrderDetailResource, OrderDetailDTO>(resource);
            var result = await orderDetailService.SaveAsync(orderDetail);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.OrderDetail);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveOrderDetailResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var orderDetail = mapper.Map<SaveOrderDetailResource, OrderDetailDTO>(resource);
            var result = await orderDetailService.UpdateAsync(id, orderDetail);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.OrderDetail);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await orderDetailService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.OrderDetail);
        }
    }
}