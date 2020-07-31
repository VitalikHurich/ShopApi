using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Context;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories;
using ShopApi.BLL.Services.Interfaces;
using ShopApi.BLL.Response;
using ShopApi.BLL.Services;
using ShopApi.BLL.DTO;
using AutoMapper;
using ShopApi.Root.Mapping;

namespace ShopApi.Root
{
    public class CompositionRoot
    {
        public CompositionRoot() { }

        public static void injectDependencies(IServiceCollection services)
        {
            services.AddDbContext<ShopContext>(options => options.UseInMemoryDatabase("Shop"));
            services.AddScoped<ShopContext>();

            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IGoodRepository, GoodRepository>();
            services.AddScoped<IRepository<Manufacturer>, ManufacturerRepository>();
            services.AddScoped<IRepository<Role>, RoleRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<OrderDetail>, OrderDetailRepository>();
            services.AddScoped<IUnitOfwork, UnitOfWork>();
            services.AddScoped<IService<CategoryDTO, CategoryResponse>, CategoryService>();
            services.AddScoped<IGoodService, GoodService>();
            services.AddScoped<IService<ManufacturerDTO, ManufacturerResponse>, ManufacturerService>();
            services.AddScoped<IService<RoleDTO, RoleResponse>, RoleService>();
            services.AddScoped<IService<UserDTO, UserResponse>, UserService>();
            services.AddScoped<IService<OrderDTO, OrderResponse>, OrderService>();
            services.AddScoped<IService<OrderDetailDTO, OrderDetailResponse>, OrderDetailService>();
            services.AddScoped<IUserRolesService, UserRolesService>();
            services.AddScoped<IAuthService, AuthService>();

            // services.AddAutoMapper(typeof(ModelToResourceProfile), typeof(ResourceToModelProfile));
        }
    }
}