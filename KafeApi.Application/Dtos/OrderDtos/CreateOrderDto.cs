using KafeApi.Application.Dtos.OrderItemDtos;

namespace KafeApi.Application.Dtos.OrderDtos;

public class CreateOrderDto
{
    public int TableId { get; set; }
    //public decimal TotalPrice { get; set; }
    //public DateTime CreatedAt { get; set; } = DateTime.Now;
    //public DateTime? UpdatedAt { get; set; }
    //public string Status { get; set; } //enum değer yazılacak
    public List<CreateOrderItemDto> OrderItems { get; set; } //enum değer yazılacak
}
