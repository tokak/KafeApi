using KafeApi.Application.Dtos.CafeInfoDtos;
using KafeApi.Application.Dtos.ResponseDtos;

namespace KafeApi.Application.Services.Abstract;

public interface ICafeInfoServices
{
    Task<ResponseDto<List<ResultCafeInfoDto>>> GetAllCafeInfos();
    Task<ResponseDto<DetailCafeInfoDto>> GetByIdCafeInfo(int id);
    Task<ResponseDto<object>> AddCafeInfo(CreateCafeInfoDto dto);
    Task<ResponseDto<object>> UpdateCafeInfo(UpdateCafeInfoDto dto);
    Task<ResponseDto<object>> DeleteCafeInfo(int id);
}
