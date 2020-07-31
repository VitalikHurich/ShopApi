using AutoMapper;
using ShopApi.BLL.DTO;
using ShopApi.Core.Domain;
using ShopApi.Resource;

namespace ShopApi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCategoryResource, CategoryDTO>();
            CreateMap<SaveGoodResource, GoodDTO>();
            CreateMap<SaveManufacturerResource, ManufacturerDTO>();
            CreateMap<SaveRoleResource, RoleDTO>();
            CreateMap<SaveUserResource, UserDTO>();
            CreateMap<SaveOrderResource, OrderDTO>();
            CreateMap<SaveOrderDetailResource, OrderDetailDTO>();
        }
    }
}