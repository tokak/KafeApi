using KafeApi.Domain.Entities;

namespace KafeApi.Application.Interfaces;

public interface ITableRepository
{
    Task<Table> GetByTableNumberAsync(int tableNumber);
}
