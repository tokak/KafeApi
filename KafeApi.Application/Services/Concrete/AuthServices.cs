using KafeApi.Application.Dtos.AuthDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.UserDtos;
using KafeApi.Application.Helpers;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;

namespace KafeApi.Application.Services.Concrete;

public class AuthServices : IAuthServices
{
    private readonly TokenHelpers _tokenHelpers;
    private readonly IUserRepository _userRepository;

    public AuthServices(TokenHelpers tokenHelpers, IUserRepository userRepository)
    {
        _tokenHelpers = tokenHelpers;
        _userRepository = userRepository;
    }

    public async Task<ResponseDto<object>> GenerateToken(LoginDto dto)
    {
        try
        {
            var userCheck = await _userRepository.CheckUser(dto.Email);
            //var checkUser = dto.Email == "admin@gmail.com" ? true : false;
            if (userCheck.Id != null)
            {
                var user = await _userRepository.CheckUserWithPassword(dto);
                if (user.Succeeded)
                {
                    var tokendto = new TokenDto()
                    {
                        Email = dto.Email,
                        Id = userCheck.Id,
                        Role = "Admin"
                    };
                    string token = _tokenHelpers.GenerateToken(tokendto);
                    return new ResponseDto<object> { Success = true, Data = token, Message = "işlem başarılı" };
                }
            }

            return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.Unauthorized, Message = "Kullanıcı bulunamadı." };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.Exception, Message = ex.Message };
        }
    }
}
