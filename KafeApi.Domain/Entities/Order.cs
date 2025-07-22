namespace KafeApi.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt{ get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Status { get; set; } //enum değer yazılacak
    public List<OrderItem> OrderItems { get; set; } //enum değer yazılacak
}
