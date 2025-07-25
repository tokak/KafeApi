using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.CafeInfoDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Application.Validators.CafeInfo;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Services.Concrete;

public class CafeInfoService : ICafeInfoServices
{
    private readonly IGenericRepository<CafeInfo> _genericRepository;
    private readonly IValidator<CreateCafeInfoDto> _addCafeInfoValidator;
    private readonly IValidator<UpdateCafeInfoDto> _updateCafeInfoValidator;
    private readonly IMapper _mapper;

    public CafeInfoService(IGenericRepository<CafeInfo> genericRepository, IMapper mapper, IValidator<CreateCafeInfoDto> addCafeInfoValidator, IValidator<UpdateCafeInfoDto> updateCafeInfoValidator)
    {
        _genericRepository = genericRepository;
        _mapper = mapper; ;
        _addCafeInfoValidator = addCafeInfoValidator;
        _updateCafeInfoValidator = updateCafeInfoValidator;
    }

    public async Task<ResponseDto<object>> AddCafeInfo(CreateCafeInfoDto dto)
    {
        try
        {
            var validate = await _addCafeInfoValidator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.ValidationError,
                    Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage).ToList())
                };
            }

            var cafeInfoMap = _mapper.Map<CafeInfo>(dto);
            await _genericRepository.AddAsync(cafeInfoMap);
            return new ResponseDto<object> { Success = true, Data = cafeInfoMap, Message = "Kayıt başarılı" };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.Exception, Message = "Bir hata oluştu" };

        }
    }

    public async Task<ResponseDto<object>> DeleteCafeInfo(int id)
    {
        try
        {
            var findCafeInfo = await _genericRepository.GetByIdAsync(id);
            if (findCafeInfo == null)
            {
                return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kayıt bulunamadı" };

            }
            await _genericRepository.DeleteAsync(findCafeInfo);
            return new ResponseDto<object> { Success = true, Data = findCafeInfo, Message = "Kayıt başarıli silindi" };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.Exception, Message = "Bir hata oluştu" };

        }
    }

    public async Task<ResponseDto<List<ResultCafeInfoDto>>> GetAllCafeInfos()
    {
        try
        {
            var listCafeInfo = await _genericRepository.GetAllAsync();
            if (listCafeInfo == null)
            {
                return new ResponseDto<List<ResultCafeInfoDto>> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kayıt bulunamadı" };

            }
            var listMap = _mapper.Map<List<ResultCafeInfoDto>>(listCafeInfo);
            return new ResponseDto<List<ResultCafeInfoDto>> { Success = true, Data = listMap, Message = "Kayıt başarıli listelendi" };
        }
        catch (Exception ex)
        {
            return new ResponseDto<List<ResultCafeInfoDto>> { Success = false, Data = null, ErrorCode = ErrorCodes.Exception, Message = "Bir hata oluştu" };

        }
    }

    public async Task<ResponseDto<DetailCafeInfoDto>> GetByIdCafeInfo(int id)
    {
        try
        {
            var findCafeInfo = await _genericRepository.GetByIdAsync(id);
            if (findCafeInfo == null)
            {
                return new ResponseDto<DetailCafeInfoDto> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kayıt bulunamadı" };

            }
            var cafeInfoMap = _mapper.Map<DetailCafeInfoDto>(findCafeInfo);
            return new ResponseDto<DetailCafeInfoDto> { Success = true, Data = cafeInfoMap, Message = "Kayıt başarıli listelendi" };
        }
        catch (Exception ex)
        {
            return new ResponseDto<DetailCafeInfoDto> { Success = false, Data = null, ErrorCode = ErrorCodes.Exception, Message = "Bir hata oluştu" };

        }
    }

    public async Task<ResponseDto<object>> UpdateCafeInfo(UpdateCafeInfoDto dto)
    {
        try
        {
            var validate = await _updateCafeInfoValidator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.ValidationError,
                    Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage).ToList())
                };
            }
            var findCafeInfo = await _genericRepository.GetByIdAsync(dto.Id);
            if (findCafeInfo== null)
            {
                return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kayıt bulunamadı" };
            }
            var cafeInfoMap = _mapper.Map(dto,findCafeInfo);
            await _genericRepository.UpdateAsync(cafeInfoMap);
            return new ResponseDto<object> { Success = true, Data = cafeInfoMap, Message = "Kayıt başarılı güncellendi." };
        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.Exception, Message = "Bir hata oluştu" };
        }

    }

}

