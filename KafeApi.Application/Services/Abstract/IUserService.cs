using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.UserDtos;

namespace KafeApi.Application.Services.Abstract;

public interface IUserService
{
    Task<ResponseDto<object>> Register(RegisterDto dto);

}
