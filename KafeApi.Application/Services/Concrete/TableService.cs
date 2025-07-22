using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.TableDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Services.Concrete;

public class TableService : ITableServices
{
    private readonly IGenericRepository<Table> _tableRepository;

    public TableService(IGenericRepository<Table> tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public Task<ResponseDto<object>> AddTable(CreateTableDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<object>> DeleteTable(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<List<ResultTableDto>>> GetAllTables()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<DetailTableDto>> GetByIdTable(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<DetailTableDto>> GetByIdTableCodeTable(string code)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<DetailTableDto>> GetByIdTableNumber(int tableNumber)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<object>> UpdateTable(UpdateTableDto dto)
    {
        throw new NotImplementedException();
    }
}
