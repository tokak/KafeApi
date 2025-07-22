using KafeApi.Application.Dtos.MenuItemDtos;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Dtos.OrderItemDtos;

public class ResultOrderItemDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
    public DetailMenuItemDto MenuItem { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
