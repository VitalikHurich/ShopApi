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
    public class OrderDetailService : IService<OrderDetailDTO, OrderDetailResponse>
    {
        private readonly IRepository<OrderDetail> orderDetailRepository;
        private readonly IUnitOfwork unitOfwork;
        private readonly IMapper mapper;
        public OrderDetailService(IRepository<OrderDetail> orderDetailRepository, IUnitOfwork unitOfwork, IMapper mapper)
        {
            this.orderDetailRepository = orderDetailRepository;
            this.unitOfwork = unitOfwork;
            this.mapper = mapper;
        }
        public async Task<OrderDetailResponse> DeleteAsync(int id)
        {
            var existingOrderDetail = await orderDetailRepository.FindByIDAsync(id);
            if(existingOrderDetail == null)
            {
                return new OrderDetailResponse("Orderdetail not found");
            }
            try
            {
                orderDetailRepository.Remove(existingOrderDetail);
                await unitOfwork.CompleteAsync();
                return new OrderDetailResponse(existingOrderDetail);
            }
            catch (Exception ex)
            {
                return new OrderDetailResponse($"Error when deleting order detail: {ex.Message}");
            }
        }

        public async Task<IEnumerable<OrderDetailDTO>> ListAsync()
        {
            var orderDetails = await orderDetailRepository.ListAsync();
            return mapper.Map<IEnumerable<OrderDetailDTO>>(orderDetails);
        }

        public async Task<OrderDetailResponse> SaveAsync(OrderDetailDTO orderDetailDTO)
        {
            OrderDetail orderDetail = mapper.Map<OrderDetail>(orderDetailDTO);
            try
            {
                await orderDetailRepository.AddASync(orderDetail);
                await unitOfwork.CompleteAsync();

                return new OrderDetailResponse(orderDetail);
            }
            catch (Exception ex)
            {
                return new OrderDetailResponse($"Error when seving the order detail: {ex.Message}");
            }
        }

        public async Task<OrderDetailResponse> UpdateAsync(int id, OrderDetailDTO orderDetailDTO)
        {
            OrderDetail orderDetail = mapper.Map<OrderDetail>(orderDetailDTO);
            var existingOrderDetail = await orderDetailRepository.FindByIDAsync(id);
            if(existingOrderDetail == null)
            {
                return new OrderDetailResponse("Order not found");
            }

            existingOrderDetail.GoodId = orderDetail.GoodId;
            existingOrderDetail.Price = orderDetail.Price;
            existingOrderDetail.Count = orderDetail.Count;

            try
            {
                orderDetailRepository.Update(existingOrderDetail);
                await unitOfwork.CompleteAsync();
                return new OrderDetailResponse(existingOrderDetail);
            }
            catch (Exception ex)
            {
                return new OrderDetailResponse($"Error when updating order detail: {ex.Message}");
            }
        }
    }
}