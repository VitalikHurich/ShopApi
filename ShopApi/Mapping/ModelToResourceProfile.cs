using AutoMapper;
using ShopApi.BLL.DTO;
using ShopApi.Core.Domain;
using ShopApi.Resource;

namespace ShopApi.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<CategoryDTO, CategoryResource>();
            CreateMap<ManufacturerDTO, ManufacturerResource>();
            CreateMap<GoodDTO, GoodResource>();
            CreateMap<RoleDTO, RoleResource>();
            CreateMap<UserDTO, UserResource>();
            CreateMap<OrderDetailDTO, OrderDetailResource>();
            CreateMap<OrderDTO, OrderResource>();
            CreateMap<CategoryDTO, SaveCategoryResource>();
        }
    }
}