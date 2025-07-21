using KafeApi.Application.Dtos.MenuItemDtos;

namespace KafeApi.Application.Dtos.CategoryDtos;

public class DetailCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ResultMenuItemDto> MenuItems { get; set; }
}
