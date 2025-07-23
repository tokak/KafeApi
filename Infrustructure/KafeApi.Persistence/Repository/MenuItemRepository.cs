using KafeApi.Application.Interfaces;
using KafeApi.Domain.Entities;
using KafeApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KafeApi.Persistence.Repository;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly AppDbContext _appDbContext;

    public MenuItemRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<MenuItem>> GetMenuItemFilterByCategoryId(int categoryId)
    {
        var result = await _appDbContext.MenuItems.Where(x=>x.CategoryId ==categoryId).ToListAsync();
        return result;
    }
}
