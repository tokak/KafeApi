using KafeApi.Application.Dtos.MenuItemDtos;

namespace KafeApi.Application.Dtos.CategoryDtos;

public class ResultCategoriesWithMenuDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<CategoriesMenuItemDto> MenuItems { get; set; }
}
