using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.OrderDtos;
using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Application.Validators.OrderItem;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Services.Concrete;

public class OrderServices : IOrderServices
{
    private readonly IGenericRepository<Order> _orderRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateOrderDto> _createOrderValidator;
    private readonly IValidator<UpdateOrderDto> _updateOrderValidator;

    public OrderServices(IGenericRepository<Order> orderRepository, IMapper mapper, IValidator<CreateOrderDto> createOrderValidator, IValidator<UpdateOrderDto> updateOrderValidator)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _createOrderValidator = createOrderValidator;
        _updateOrderValidator = updateOrderValidator;
    }

    public async Task<ResponseDto<object>> AddOrder(CreateOrderDto dto)
    {
        try
        { 
            var validate = await _createOrderValidator.ValidateAsync(dto);
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
            var orderItem = _mapper.Map<Order>(dto);
            await _orderRepository.AddAsync(orderItem);
            return new ResponseDto<object> { Success = true, Data = orderItem, Message = "Sipariş oluşturuldu." };
        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Data = null, Message = "Bir hata oluştu.", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> DeleteOrder(int id)
    {
        try
        {
            var getOrder = await _orderRepository.GetByIdAsync(id);
            if (getOrder == null)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Sipariş bulunamadı", ErrorCode = ErrorCodes.NotFound };
            }

            await _orderRepository.DeleteAsync(getOrder);
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

    public async Task<ResponseDto<List<ResulOrderDto>>> GetAllOrders()
    {
        try
        {
            var orderList = await _orderRepository.GetAllAsync();
            if (orderList.Count() == 0)
            {
                return new ResponseDto<List<ResulOrderDto>> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kayıt bulunamadı." };
            }
            var orderListMap = _mapper.Map<List<ResulOrderDto>>(orderList);
            return new ResponseDto<List<ResulOrderDto>> { Success = true, Data = orderListMap, Message = "işlem başarılı" };
        }
        catch (Exception ex)
        {
            return new ResponseDto<List<ResulOrderDto>>
            {
                Success = false,
                Data = null,
                ErrorCode = ErrorCodes.Exception,
                Message = "Bir sorun oluştu."
            };
        }
    }

    public async Task<ResponseDto<DetailOrderDto>> GetByOrderId(int id)
    {
        try
        {
            var getOrderById = await _orderRepository.GetByIdAsync(id);
            if (getOrderById == null)
            {
                return new ResponseDto<DetailOrderDto> { Success = false, Message = "Sipariş Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
            }
            var result = _mapper.Map<DetailOrderDto>(getOrderById);
            return new ResponseDto<DetailOrderDto> { Success = true, Data = result, Message = "işlem başarılı" };
        }
        catch (Exception ex)
        {
            return new ResponseDto<DetailOrderDto> { Success = false, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto)
    {
        try
        {
            var validate = await _updateOrderValidator.ValidateAsync(dto);
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
            var checkOrder = await _orderRepository.GetByIdAsync(dto.Id);
            if (checkOrder == null)
            {
                return new ResponseDto<object> { Success = false, Message = "Sipariş bulunamadı.", Data = null, ErrorCode = ErrorCodes.NotFound };
            }
            var orderMap = _mapper.Map(dto, checkOrder);
            await _orderRepository.UpdateAsync(orderMap);
            return new ResponseDto<object> { Success = true, Message = "Sipariş güncellendi.", Data = orderMap };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir hata oluştu.", Data = null, ErrorCode = ErrorCodes.Exception };
        }
    }
}
