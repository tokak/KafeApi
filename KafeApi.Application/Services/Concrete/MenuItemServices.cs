using KafeApi.Application.Dtos.MenuItemDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Services.Concrete;

public class MenuItemServices : IMenuItemServices
{
    private readonly IGenericRepository<MenuItem> _menuItemRepository;

    public MenuItemServices(IGenericRepository<MenuItem> menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    public Task AddMenuItem(CreateMenuItemDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMenuItem(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ResultMenuItemDto>> GetAllMenuItems()
    {
        throw new NotImplementedException();
    }

    public Task<DetailMenuItemDto> GetByIdMenuItem(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMenuItem(UpdateMenuItemDto dto)
    {
        throw new NotImplementedException();
    }
}
