using KafeApi.Application.Dtos.MenuItemDtos;

namespace KafeApi.Application.Services.Abstract;

public interface IMenuItemServices
{
    Task<List<ResultMenuItemDto>> GetAllMenuItems();
    Task<DetailMenuItemDto> GetByIdMenuItem(int id);
    Task AddMenuItem(CreateMenuItemDto dto);
    Task UpdateMenuItem(UpdateMenuItemDto dto);
    Task DeleteMenuItem(int id);
}
