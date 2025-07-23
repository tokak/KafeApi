using KafeApi.Domain.Entities;

namespace KafeApi.Application.Interfaces;

public interface IMenuItemRepository
{
    Task<List<MenuItem>> GetMenuItemFilterByCategoryId(int categoryId);
}
