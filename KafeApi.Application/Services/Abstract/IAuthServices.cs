using KafeApi.Application.Dtos.AuthDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.UserDtos;

namespace KafeApi.Application.Services.Abstract;

//token işlemlerinde okuma işlemi yapılınca auth service kullanılacak Kullanıcı yönetiminde identity user service kullanılacak
public interface IAuthServices
{
    Task<ResponseDto<object>> GenerateToken(LoginDto dto);
}
