using KafeApi.Application.Dtos.UserDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost()]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _userService.Register(dto);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _userService.CreateRole(roleName);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleToUser(string email,string roleName)
        {
            var result = await _userService.AddToRole(email,roleName);
            return CreateResponse(result);
        }
        [HttpPost("registerdefault")]
        public async Task<IActionResult> RegisterDefault(RegisterDto dto)
        {
            var result = await _userService.RegisterDefault(dto);
            return CreateResponse(result);
        }
    }
}
