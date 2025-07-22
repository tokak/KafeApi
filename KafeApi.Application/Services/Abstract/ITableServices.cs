using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.TableDtos;

namespace KafeApi.Application.Services.Abstract;

public interface ITableServices
{
    Task<ResponseDto<List<ResultTableDto>>> GetAllTables();
    Task<ResponseDto<DetailTableDto>> GetByIdTable(int id);
    Task<ResponseDto<DetailTableDto>> GetByTableNumber(int tableNumber);
    Task<ResponseDto<object>> AddTable(CreateTableDto dto);
    Task<ResponseDto<object>> UpdateTable(UpdateTableDto dto);
    Task<ResponseDto<object>> DeleteTable(int id);
    Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTables();
    Task<ResponseDto<object>> UpdateTableStatusById(int id);
    Task<ResponseDto<object>> UpdateTablesStatusByTableNumber(int tableNumber);

}
