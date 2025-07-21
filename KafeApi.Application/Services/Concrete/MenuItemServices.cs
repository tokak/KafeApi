using AutoMapper;
using KafeApi.Application.Dtos.MenuItemDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Services.Concrete;

public class MenuItemServices : IMenuItemServices
{
    private readonly IGenericRepository<MenuItem> _menuItemRepository;
    private readonly IMapper _mapper;

    public MenuItemServices(IGenericRepository<MenuItem> menuItemRepository, IMapper mapper)
    {
        _menuItemRepository = menuItemRepository;
        _mapper = mapper;
    }

    public async Task AddMenuItem(CreateMenuItemDto dto)
    {
        var addMenuItem = _mapper.Map<MenuItem>(dto);
        await _menuItemRepository.AddAsync(addMenuItem);

    }

    public async Task DeleteMenuItem(int id)
    {
        var removeMenuItem = await _menuItemRepository.GetByIdAsync(id);
        await _menuItemRepository.DeleteAsync(removeMenuItem);
    }

    public async Task<List<ResultMenuItemDto>> GetAllMenuItems()
    {
        var menuItems = await _menuItemRepository.GetAllAsync();
        var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
        return result;
    }

    public async Task<DetailMenuItemDto> GetByIdMenuItem(int id)
    {
        var menuItem = await _menuItemRepository.GetByIdAsync(id);
        var result = _mapper.Map<DetailMenuItemDto>(menuItem);
        return result;
    }

    public async Task UpdateMenuItem(UpdateMenuItemDto dto)
    {
        var menuItem = _mapper.Map<MenuItem>(dto);
        await _menuItemRepository.UpdateAsync(menuItem);
    }
}
