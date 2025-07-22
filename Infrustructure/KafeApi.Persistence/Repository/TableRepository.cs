using KafeApi.Application.Interfaces;
using KafeApi.Domain.Entities;
using KafeApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KafeApi.Persistence.Repository;

public class TableRepository : ITableRepository
{
    private readonly AppDbContext _appDbContext;

    public TableRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Table> GetByTableNumberAsync(int tableNumber)
    {
        var result = await _appDbContext.Tables.FirstOrDefaultAsync(x=>x.TableNumber == tableNumber);
        return result;
    }
}
