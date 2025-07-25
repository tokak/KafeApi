using FluentValidation;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.UserDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.Application.Services.Concrete;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<RegisterDto> _registerValidator;

    public UserService(IUserRepository userRepository, IValidator<RegisterDto> registerValidator)
    {
        _userRepository = userRepository;
        _registerValidator = registerValidator;
    }

    public async Task<ResponseDto<object>> AddToRole(string email, string roleName)
    {
        try
        {
            var result = await _userRepository.AddRoleToUserAsync(email,roleName);
            if (result)
            {
                return new ResponseDto<object> { Success = true, Data = result, Message = "Rol ataması yapıldı." };
            }

            return new ResponseDto<object> { Success = false, ErrorCode = ErrorCodes.BadRequest, Data = null, Message = "Rol ataması yapılırken bir hata oluştu." };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Data = null, Message = ex.Message, ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> CreateRole(string roleName)
    {

        try
        {
            var result = await _userRepository.CreateRoleAsync(roleName);
            if (result)
            {
                return new ResponseDto<object> { Success = true, Data = result, Message = "Rol oluşturuldu." };
            }
           
            return new ResponseDto<object> { Success = false,ErrorCode = ErrorCodes.BadRequest, Data = null, Message = "Rol oluşturulamadı." };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Data = null, Message = ex.Message, ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> Register(RegisterDto dto)
    {
        try
        {
            var validate = await _registerValidator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kullanıcı bulunamadı." };
            }
            var result = await _userRepository.RegisterAsync(dto);
            if (result.Succeeded)
            {
                return new ResponseDto<object> { Success = true, Data = dto, Message = "işlem başarılı." };
            }
            return new ResponseDto<object> { Success = false, Data = null, Message = result.Errors.FirstOrDefault().Description };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Data = null, Message = ex.Message ,ErrorCode=ErrorCodes.Exception};
        }
    }
   
    public async Task<ResponseDto<object>> RegisterDefault(RegisterDto dto)
    {
        try
        {
            var validate = await _registerValidator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kullanıcı bulunamadı." };
            }
            var result = await _userRepository.RegisterAsync(dto);
            if (result.Succeeded)
            {
                var roleResult = await _userRepository.AddRoleToUserAsync(dto.Email, "User");
                if (roleResult)
                {
                    return new ResponseDto<object> { Success = true, Data = dto, Message = "işlem başarılı." };
                }
                else
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Kullanıcı oluşturuldu rol ataması yapılırken hata oluştu.",ErrorCode = ErrorCodes.BadRequest };

                }
            }
            return new ResponseDto<object> { Success = false, Data = null, Message = result.Errors.FirstOrDefault().Description };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Data = null, Message = ex.Message, ErrorCode = ErrorCodes.Exception };
        }
    }
}
