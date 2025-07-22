using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.TableDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Services.Concrete;

public class TableService : ITableServices
{
    private readonly IGenericRepository<Table> _tableRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateTableDto> _createTableValidator;
    private readonly IValidator<UpdateTableDto> _updateTableValidator;
    public readonly ITableRepository _tableRepository2;

    public TableService(IGenericRepository<Table> tableRepository, IMapper mapper, IValidator<CreateTableDto> createTableValidator, IValidator<UpdateTableDto> updateTableValidator, ITableRepository tableRepository2)
    {
        _tableRepository = tableRepository;
        _mapper = mapper;
        _createTableValidator = createTableValidator;
        _updateTableValidator = updateTableValidator;
        _tableRepository2 = tableRepository2;
    }

    public async Task<ResponseDto<object>> AddTable(CreateTableDto dto)
    {
        try
        {
            var validate = await _createTableValidator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    ErrorCodes = ErrorCodes.ValidationError,
                    Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage).ToList())
                };
            }
            var checkTable = await _tableRepository.GetByIdAsync(dto.TableNumber);
            if (checkTable != null)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Eklemek istediğiniz masa numarası mevcuttur",
                    ErrorCodes = ErrorCodes.DublicationError
                };
            }
            var tableMap = _mapper.Map<Table>(dto);
            await _tableRepository.AddAsync(tableMap);
            return new ResponseDto<object> { Success = true, Data = tableMap, Message = "Kayıt başarılı" };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Data = null, ErrorCodes = ErrorCodes.Exception, Message = "Bir hata oluştu." };
        }
    }

    public async Task<ResponseDto<object>> DeleteTable(int id)
    {
        try
        {
            var findTable = await _tableRepository.GetByIdAsync(id);
            if (findTable == null)
            {
                return new ResponseDto<object> { Success = false, Data = null, ErrorCodes = ErrorCodes.NotFound, Message = "Kayıt bulunamadı." };
            }
            await _tableRepository.DeleteAsync(findTable);
            return new ResponseDto<object> { Success = true, Data = findTable, Message = "Kayıt silindi." };

        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Data = null, ErrorCodes = ErrorCodes.Exception, Message = "Bir hata oluştu." };
        }
    }

    public async Task<ResponseDto<List<ResultTableDto>>> GetAllTables()
    {
        try
        {
            var tables = await _tableRepository.GetAllAsync();
            if (tables.Count() == 0)
            {
                return new ResponseDto<List<ResultTableDto>>() { Success = true, Data = null, ErrorCodes = ErrorCodes.NotFound, Message = "Masalar bulunamadı." };
            }
            var result = _mapper.Map<List<ResultTableDto>>(tables);
            return new ResponseDto<List<ResultTableDto>> { Success = true, Data = result };
        }
        catch (Exception ex)
        {
            return new ResponseDto<List<ResultTableDto>>()
            {
                Success = false,
                Data = null,
                ErrorCodes = ErrorCodes.Exception,
                Message = "Bir hata oluştu."
            };
        }
    }

    public async Task<ResponseDto<DetailTableDto>> GetByIdTable(int id)
    {
        try
        {
            var getTableById = await _tableRepository.GetByIdAsync(id);
            if (getTableById == null)
            {
                return new ResponseDto<DetailTableDto> { Success = false, Data = null, ErrorCodes = ErrorCodes.NotFound, Message = "Kayıt bulunamadı." };
            }
            var result = _mapper.Map<DetailTableDto>(getTableById);
            return new ResponseDto<DetailTableDto> { Success = true, Data = result, Message = "İşlem başarılı" };
        }
        catch (Exception ex)
        {
            return new ResponseDto<DetailTableDto>
            {
                Success = false,
                Data = null,
                ErrorCodes = ErrorCodes.Exception,
                Message = "Bir hata oluştu."
            };
        }
    }


    public async Task<ResponseDto<DetailTableDto>> GetByTableNumber(int tableNumber)
    {
        try
        {
            var table = await _tableRepository2.GetByTableNumberAsync(tableNumber);
            if (table == null)
            {
                return new ResponseDto<DetailTableDto>
                {
                    Success = false,
                    Data = null,
                    ErrorCodes = ErrorCodes.NotFound,
                    Message = "Kayıt bulunamadı."
                };
            }
            var result = _mapper.Map<DetailTableDto>(table);
            return new ResponseDto<DetailTableDto> { Success = true, Data = result, Message = "işlem başarılı" };
        }
        catch (Exception ex)
        {
            return new ResponseDto<DetailTableDto>
            {
                Success = false,
                Data = null,
                ErrorCodes = ErrorCodes.Exception,
                Message = "Bir hata oluştu."
            };
        }
    }

    public async Task<ResponseDto<object>> UpdateTable(UpdateTableDto dto)
    {
        try
        {
            var validate = await _updateTableValidator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object> { Success = false, Data = null, ErrorCodes = ErrorCodes.ValidationError, Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage).ToList()) };
            }
            var findTable = await _tableRepository.GetByIdAsync(dto.Id);
            if (findTable == null)
            {
                return new ResponseDto<object> { Success = false, Data = null, ErrorCodes = ErrorCodes.NotFound, Message = "Kayıt bulunamadı." };
            }

            var table = _mapper.Map(dto, findTable);
            await _tableRepository.UpdateAsync(table);
            return new ResponseDto<object> { Success = true, Data = table, Message = "İşlem başarılı." };

        }
        catch (Exception ex)
        {
            return new ResponseDto<object>
            {
                Success = false,
                Data = null,
                ErrorCodes = ErrorCodes.Exception,
                Message = "Bir hata oluştu"
            };
        }
    }
}
