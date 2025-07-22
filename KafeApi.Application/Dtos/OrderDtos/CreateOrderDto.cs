using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Dtos.OrderDtos;

public class CreateOrderDto
{
    public int TableId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Status { get; set; } //enum değer yazılacak
    public List<CreateOrderItemDto> OrderItems { get; set; } //enum değer yazılacak
}
