namespace KafeApi.Application.Dtos.MenuItemDtos;

public class CreateMenuItemDto
{
    public string Name { get; set; }
    public string Desription { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public bool IsAvailable { get; set; }
    public int CategoryId { get; set; }
}
