using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.CategoryDtos;
using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;
using Microsoft.Extensions.Options;

namespace KafeApi.Application.Services.Concrete;

public class OrderItemServices : IOrderItemServices
{
    private readonly IGenericRepository<OrderItem> _orderItemRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateOrderItemDto> _createOrderItemValidator;
    private readonly IValidator<UpdateOrderItemDto> _updateOrderItemValidotor;
    public OrderItemServices(IGenericRepository<OrderItem> orderItemRepository, IMapper mapper, IValidator<CreateOrderItemDto> createOrderItemValidator, IValidator<UpdateOrderItemDto> updateOrderItemValidotor)
    {
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
        _createOrderItemValidator = createOrderItemValidator;
        _updateOrderItemValidotor = updateOrderItemValidotor;
    }

    public async Task<ResponseDto<object>> AddOrderItem(CreateOrderItemDto dto)
    {
        try
        {
            var validate = await _createOrderItemValidator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = string.Join(" | ", validate.Errors.Select(x => x.ErrorMessage).ToList()),
                    ErrorCode = ErrorCodes.ValidationError,
                };
            }
            var orderItem = _mapper.Map<OrderItem>(dto);
            await _orderItemRepository.AddAsync(orderItem);
            return new ResponseDto<object> { Success = true, Data = orderItem, Message = "Sipariş oluşturuldu." };
        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Data = null, Message = "Bir hata oluştu.", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> DeleteOrderItem(int id)
    {
        try
        {
            var getOrder = await _orderItemRepository.GetByIdAsync(id);
            if (getOrder == null)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Sipariş bulunamadı", ErrorCode = ErrorCodes.NotFound };
            }

            await _orderItemRepository.DeleteAsync(getOrder);
            return new ResponseDto<object> { Success = true, Data = getOrder, Message = "Sipariş silindi" };

        }
        catch (Exception ex)
        {
            return new ResponseDto<object>
            {
                Success = false,
                Message = "Bir Hata Oluştu.",
                ErrorCode = ErrorCodes.Exception
            };
        }
    }

    public async Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems()
    {
        try
        {
            var orderItemList = await _orderItemRepository.GetAllAsync();
            if (orderItemList.Count() == 0)
            {
                return new ResponseDto<List<ResultOrderItemDto>> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kayıt bulunamadı." };
            }
            var orderItemListMap = _mapper.Map<List<ResultOrderItemDto>>(orderItemList);
            return new ResponseDto<List<ResultOrderItemDto>> { Success = true, Data = orderItemListMap, Message = "işlem başarılı" };
        }
        catch (Exception ex)
        {
            return new ResponseDto<List<ResultOrderItemDto>>
            {
                Success = false,
                Data = null,
                ErrorCode = ErrorCodes.Exception,
                Message = "Bir sorun oluştu."
            };
        }
    }

    public async Task<ResponseDto<DetailOrderItemDto>> GetByOrderItemId(int id)
    {
        try
        {
            var getOrderById = await _orderItemRepository.GetByIdAsync(id);
            if (getOrderById == null)
            {
                return new ResponseDto<DetailOrderItemDto> { Success = false, Message = "Sipariş Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
            }
            var result = _mapper.Map<DetailOrderItemDto>(getOrderById);
            return new ResponseDto<DetailOrderItemDto> { Success = true, Data = result ,Message="işlem başarılı"};
        }
        catch (Exception ex)
        {
            return new ResponseDto<DetailOrderItemDto> { Success = false, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> UpdateOrderItem(UpdateOrderItemDto dto)
    {
        try
        {
            var validate = await _updateOrderItemValidotor.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = string.Join(" , ", validate.Errors.Select(x => x.ErrorMessage).ToList()),
                    Data = null,
                    ErrorCode = ErrorCodes.ValidationError
                };
            }
            var checkOrderItem = await _orderItemRepository.GetByIdAsync(dto.Id);
            if (checkOrderItem == null)
            {
                return new ResponseDto<object> { Success = false, Message = "Sipariş bulunamadı.", Data = null, ErrorCode = ErrorCodes.NotFound };
            }
            var category = _mapper.Map(dto, checkOrderItem);
            await _orderItemRepository.UpdateAsync(category);
            return new ResponseDto<object> { Success = true, Message = "Sipariş güncellendi.", Data = category };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir hata oluştu.", Data = null, ErrorCode = ErrorCodes.Exception };
        }
    }
}
