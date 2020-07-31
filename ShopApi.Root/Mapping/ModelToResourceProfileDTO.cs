using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ShopApi.BLL.DTO;
using ShopApi.Core.Domain;
using ShopApi.DAL.Models;
using ShopApi.Root.Converters;

namespace ShopApi.Root.Mapping
{
    public class ModelToResourceProfileDTO : Profile
    {
        public ModelToResourceProfileDTO()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<Manufacturer, ManufacturerDTO>();
            CreateMap<Good, GoodDTO>().ForMember(dest => dest.CategoryName, src => src.MapFrom(x => x.Category.CategoryName))
                                            .ForMember(dest => dest.ManufacturerName, src => src.MapFrom(x => x.Manufacturer.ManufacturerName));
            CreateMap<PagedList<Good>, PagedList<GoodDTO>>().ConvertUsing<PagedListConverter>();
            CreateMap<Role, RoleDTO>();
            CreateMap<User, UserDTO>().ForMember(dest => dest.Role, src => src.MapFrom(x => x.UserRoles.Select(y => y.Role.RoleName)))
                                            .ForMember(dest => dest.OrderList, src => src.MapFrom(x => x.Orders.Select(y => y.OrderId)));
            CreateMap<OrderDetail, OrderDetailDTO>().ForMember(dest => dest.GoodName, src => src.MapFrom(x => x.Good.GoodName))
                                                         .ForMember(dest => dest.UnitPrice, src => src.MapFrom(x => x.Good.GoodPriceActual));
            CreateMap<Order, OrderDTO>().ForMember(dest => dest.Goods, src => src.MapFrom(x => x.OrderDetails.Select(y => y.Good.GoodName)));
        }
    }
}