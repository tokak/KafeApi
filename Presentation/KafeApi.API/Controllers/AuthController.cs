using KafeApi.Application.Dtos.AuthDtos;
using KafeApi.Application.Dtos.UserDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : BaseController
{
    private readonly IAuthServices _authServices;

    public AuthController(IAuthServices authServices)
    {
        _authServices = authServices;
    }
    [HttpPost("generatetoken")]
    public async Task<IActionResult> GenerateToken(LoginDto dto)
    {
        var result = await _authServices.GenerateToken(dto);
        return CreateResponse(result);
    }

}
