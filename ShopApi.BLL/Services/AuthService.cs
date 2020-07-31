using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using ShopApi.BLL.DTO;
using ShopApi.BLL.Extensions;
using ShopApi.BLL.Helpers;
using ShopApi.BLL.Services.Interfaces;
using ShopApi.DAL.Models;
using ShopApi.DAL.Repositories.RepositoriesInterfaces;

namespace ShopApi.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings appSettings;
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;
        public AuthService(IOptions<AppSettings> appSettings, IRepository<User> userRepository, IMapper mapper)
        {
            this.appSettings = appSettings.Value;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<UserDTO> Authenticate(string login, string password)
        {
            var user = (await userRepository.ListAsync())
                                .SingleOrDefault(usr => usr.Login == login &&
                                                        usr.Password == password);  
            if (user == null)
                return null;

            user.GenerateToken(appSettings.Secret, appSettings.ExpiresMinutes);

            return mapper.Map<UserDTO>(user);
        }
    }
}