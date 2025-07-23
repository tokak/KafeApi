using KafeApi.Application.Dtos.OrderItemDtos;

namespace KafeApi.Application.Dtos.OrderDtos;

public class UpdateOrderDto
{
    public int Id { get; set; }
    public int TableId { get; set; }
    //public decimal TotalPrice { get; set; }
    //public DateTime CreatedAt { get; set; }
    //public DateTime? UpdatedAt { get; set; }
    //public string Status { get; set; } //enum değer yazılacak
    public List<UpdateOrderDto> OrderItems { get; set; } //enum değer yazılacak
}
