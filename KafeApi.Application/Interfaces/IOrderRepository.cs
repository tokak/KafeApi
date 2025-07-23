using KafeApi.Domain.Entities;

namespace KafeApi.Application.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOrderWithDetailAsync();
    Task<Order> GetOrderWByIdWithDetailAsync(int orderId);
}
