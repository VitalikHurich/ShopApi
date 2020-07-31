using AutoMapper;
using ShopApi.BLL.DTO;
using ShopApi.Core.Domain;
using ShopApi.DAL.Models;

namespace ShopApi.Root.Mapping
{
    public class ResourceToModelProfileDTO : Profile
    {
        public ResourceToModelProfileDTO()
        {
            CreateMap<CategoryDTO, Category>();
            CreateMap<GoodDTO, Good>();
            CreateMap<PagedList<GoodDTO>, PagedList<Good>>();
            CreateMap<ManufacturerDTO, Manufacturer>();
            CreateMap<RoleDTO, Role>();
            CreateMap<UserDTO, User>();
            CreateMap<OrderDTO, Order>();
            CreateMap<OrderDetailDTO, OrderDetail>();
        }
    }
}