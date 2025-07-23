using KafeApi.Application.Interfaces;
using KafeApi.Domain.Entities;
using KafeApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KafeApi.Persistence.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _appDbContext;

    public OrderRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Order>> GetAllOrderWithDetailAsync()
    {
        var result = await _appDbContext.Orders.Include(x=>x.OrderItems).ThenInclude(x=>x.MenuItem).ThenInclude(c=>c.Category).ToListAsync();
        return result;
    }
    public async Task<Order> GetOrderWByIdWithDetailAsync(int orderId)
    {
        var result = await _appDbContext.Orders.Include(x => x.OrderItems).ThenInclude(x => x.MenuItem).ThenInclude(c => c.Category).Where(x => x.Id == orderId).FirstOrDefaultAsync(); ;
        return result;
    }
}
