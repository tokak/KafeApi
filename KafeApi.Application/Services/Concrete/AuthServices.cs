using KafeApi.Application.Dtos.AuthDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Helpers;
using KafeApi.Application.Services.Abstract;

namespace KafeApi.Application.Services.Concrete;

public class AuthServices : IAuthServices
{
    private readonly TokenHelpers _tokenHelpers;

    public AuthServices(TokenHelpers tokenHelpers)
    {
        _tokenHelpers = tokenHelpers;
    }

    public async Task<ResponseDto<object>> GenerateToken(TokenDto dto)
    {
        try
        {
            var checkUser = dto.Email == "admin@gmail.com" ? true : false;
            if (checkUser)
            {
                string token = _tokenHelpers.GenerateToken(dto);
                return new ResponseDto<object> { Success = true, Data = token, Message = "işlem başarılı" };
            }

            return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.Unauthorized, Message = "Kullanıcı bulunamadı." };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.Exception, Message = ex.Message };
        }
    }
}
