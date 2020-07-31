using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.BLL.DTO;
using ShopApi.BLL.Services.Interfaces;
using ShopApi.Resource;

namespace ShopApi.Controllers
{
    [Route("/api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService userService;
        private readonly IMapper mapper;
        public AuthController(IAuthService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] SaveUserResource user)
        {
            var userA = await userService.Authenticate(user.Login, user.Password);
            var result = mapper.Map<UserDTO, UserResource>(userA);
            var response = new ResponseData
            {
                Success = result != null,
                Message = result != null ? "" : "Incorrect login or password",
                Data = result
            };
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
    }
}