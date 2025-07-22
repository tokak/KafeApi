using KafeApi.Application.Dtos.MenuItemDtos;
using KafeApi.Application.Dtos.ResponseDtos;

namespace KafeApi.Application.Services.Abstract;

public interface IMenuItemServices
{
    Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems();
    Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id);
    Task<ResponseDto<object>> AddMenuItem(CreateMenuItemDto dto);
    Task<ResponseDto<object>> UpdateMenuItem(UpdateMenuItemDto dto);
    Task<ResponseDto<object>> DeleteMenuItem(int id);
}
