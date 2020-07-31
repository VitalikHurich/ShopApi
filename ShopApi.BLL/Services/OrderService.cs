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
    public class OrderService : IService<OrderDTO, OrderResponse>
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IUnitOfwork unitOfwork;
        private readonly IMapper mapper;
        public OrderService(IRepository<Order> orderRepository, IUnitOfwork unitOfwork, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.unitOfwork = unitOfwork;
            this.mapper = mapper;
        }
        public async Task<OrderResponse> DeleteAsync(int id)
        {
            var existingOrder = await orderRepository.FindByIDAsync(id);
            if(existingOrder == null)
            {
                return new OrderResponse("Order not found");
            }
            try
            {
                orderRepository.Remove(existingOrder);
                await unitOfwork.CompleteAsync();
                return new OrderResponse(existingOrder);
            }
            catch (Exception ex)
            {
                return new OrderResponse($"Error when deleting order: {ex.Message}");
            }
        }

        public async Task<IEnumerable<OrderDTO>> ListAsync()
        {
            var orders = await orderRepository.ListAsync();
            return mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderResponse> SaveAsync(OrderDTO orderDTO)
        {
            Order order = mapper.Map<Order>(orderDTO);
            try
            {
                await orderRepository.AddASync(order);
                await unitOfwork.CompleteAsync();

                return new OrderResponse(order);
            }
            catch (Exception ex)
            {
                return new OrderResponse($"Error when seving the order: {ex.Message}");
            }
        }

        public async Task<OrderResponse> UpdateAsync(int id, OrderDTO orderDTO)
        {
            Order order = mapper.Map<Order>(orderDTO);
            var existingOrder = await orderRepository.FindByIDAsync(id);
            if(existingOrder == null)
            {
                return new OrderResponse("Order not found");
            }

            existingOrder.UserId = order.UserId;
            existingOrder.OrderDetails = order.OrderDetails;

            try
            {
                orderRepository.Update(existingOrder);
                await unitOfwork.CompleteAsync();
                return new OrderResponse(existingOrder);
            }
            catch (Exception ex)
            {
                return new OrderResponse($"Error when updating order: {ex.Message}");
            }
        }
    }
}