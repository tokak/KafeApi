using KafeApi.Application.Dtos.OrderItemDtos;

namespace KafeApi.Application.Dtos.OrderDtos;

public class AddOrderItemByOrderDto
{
    public int OrderId { get; set; }
    public CreateOrderItemDto OrderItem { get; set; }
}
