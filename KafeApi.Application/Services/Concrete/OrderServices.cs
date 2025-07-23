using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.OrderDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Services.Concrete;

public class OrderServices : IOrderServices
{
    private readonly IGenericRepository<Order> _orderRepository;
    private readonly IGenericRepository<OrderItem> _orderItemRepository;
    private readonly IGenericRepository<MenuItem> _menuItemRepository;
    private readonly IGenericRepository<Table> _tableRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateOrderDto> _createOrderValidator;
    private readonly IValidator<UpdateOrderDto> _updateOrderValidator;
    private readonly IOrderRepository _orderRepository2;

    public OrderServices(IGenericRepository<Order> orderRepository, IMapper mapper, IValidator<CreateOrderDto> createOrderValidator, IValidator<UpdateOrderDto> updateOrderValidator, IGenericRepository<OrderItem> orderItemRepository, IGenericRepository<MenuItem> menuItemRepository, IOrderRepository orderRepository2, IGenericRepository<Table> tableRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _createOrderValidator = createOrderValidator;
        _updateOrderValidator = updateOrderValidator;
        _orderItemRepository = orderItemRepository;
        _menuItemRepository = menuItemRepository;
        _orderRepository2 = orderRepository2;
        _tableRepository = tableRepository;
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
            orderItem.Status = OrderStatus.Hazirlaniyor;
            orderItem.CreatedAt = DateTime.Now;
            decimal totalPrice = 0;
            foreach (var item in orderItem.OrderItems)
            {
                item.MenuItem = await _menuItemRepository.GetByIdAsync(item.MenuItemId);
                item.Price = item.MenuItem.Price * item.Quantity;
                totalPrice += item.Price;
            }
            orderItem.TotalPrice = totalPrice;
            await _orderRepository.AddAsync(orderItem);
            var table = await _tableRepository.GetByIdAsync(dto.TableId);
            table.IsActive = false;
            await _tableRepository.UpdateAsync(table);

            return new ResponseDto<object> { Success = true, Data = orderItem, Message = "Sipariş oluşturuldu." };
        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Data = null, Message = "Bir hata oluştu.", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> AddOrderItemByOrderId(AddOrderItemByOrderDto dto)
    {
        try
        {
            var checkOrder = await _orderRepository.GetByIdAsync(dto.OrderId);
            var orderItems = await _orderItemRepository.GetAllAsync();
            if (checkOrder == null)
            {
                return new ResponseDto<object> { Success = false, Message = "Sipariş bulunamadı.", Data = null, ErrorCode = ErrorCodes.NotFound };
            }
            var orderMap = _mapper.Map<OrderItem>(dto.OrderItem);
            checkOrder.OrderItems.Add(orderMap);
            await _orderItemRepository.UpdateAsync(orderMap);
            return new ResponseDto<object> { Success = true, Message = "Sipariş güncellendi.", Data = orderMap };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir hata oluştu.", Data = null, ErrorCode = ErrorCodes.Exception };
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
            var orderItemList = await _orderItemRepository.GetAllAsync();
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

    public async Task<ResponseDto<List<ResulOrderDto>>> GetAllOrdersWithDetail()
    {
        try
        {
            var orderList = await _orderRepository.GetAllAsync();
            var orderItemList = await _orderRepository2.GetAllOrderWithDetailAsync();
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
            var getOrderById = await _orderRepository2.GetOrderWByIdWithDetailAsync(id);
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
            orderMap.UpdatedAt = DateTime.Now;
            decimal totalPrice = 0;
            foreach (var item in orderMap.OrderItems)
            {
                item.MenuItem = await _menuItemRepository.GetByIdAsync(item.MenuItemId);
                item.Price = item.MenuItem.Price * item.Quantity;
                totalPrice += item.Price;
            }
            orderMap.TotalPrice = totalPrice;
            await _orderRepository.UpdateAsync(orderMap);
            return new ResponseDto<object> { Success = true, Message = "Sipariş güncellendi.", Data = orderMap };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir hata oluştu.", Data = null, ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> UpdateOrderStatusHazir(int orderId)
    {
        try
        {

            var orderFind = await _orderRepository.GetByIdAsync(orderId);
            if (orderFind == null)
            {
                return new ResponseDto<object> { Success = false, Message = "Sipariş bulunamadı.", Data = null, ErrorCode = ErrorCodes.NotFound };
            }
            orderFind.Status = OrderStatus.Hazir;
            await _orderRepository.UpdateAsync(orderFind);
            return new ResponseDto<object> { Success = true, Message = "Sipariş durumu hazır olarak güncellendi.", Data = orderFind };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir hata oluştu.", Data = null, ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> UpdateOrderStatusIptalEdildi(int orderId)
    {
        try
        {

            var orderFind = await _orderRepository.GetByIdAsync(orderId);
            if (orderFind == null)
            {
                return new ResponseDto<object> { Success = false, Message = "Sipariş bulunamadı.", Data = null, ErrorCode = ErrorCodes.NotFound };
            }
            orderFind.Status = OrderStatus.IptalEdildi;
            await _orderRepository.UpdateAsync(orderFind);
            return new ResponseDto<object> { Success = true, Message = "Sipariş durumu iptal olarak güncellendi.", Data = orderFind };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir hata oluştu.", Data = null, ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> UpdateOrderStatusIptalOdendi(int orderId)
    {
        try
        {

            var orderFind = await _orderRepository.GetByIdAsync(orderId);
            if (orderFind == null)
            {
                return new ResponseDto<object> { Success = false, Message = "Sipariş bulunamadı.", Data = null, ErrorCode = ErrorCodes.NotFound };
            }
            orderFind.Status = OrderStatus.Odendi;
            await _orderRepository.UpdateAsync(orderFind);
            var table = await _tableRepository.GetByIdAsync(orderFind.TableId);
            table.IsActive = true;
            return new ResponseDto<object> { Success = true, Message = "Sipariş durumu ödendi.", Data = orderFind };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir hata oluştu.", Data = null, ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> UpdateOrderStatusTeslimEdildi(int orderId)
    {
        try
        {

            var orderFind = await _orderRepository.GetByIdAsync(orderId);
            if (orderFind == null)
            {
                return new ResponseDto<object> { Success = false, Message = "Sipariş bulunamadı.", Data = null, ErrorCode = ErrorCodes.NotFound };
            }
            orderFind.Status = OrderStatus.TeslimEdildi;
            await _orderRepository.UpdateAsync(orderFind);
            return new ResponseDto<object> { Success = true, Message = "Sipariş durumu teslim edildi.", Data = orderFind };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir hata oluştu.", Data = null, ErrorCode = ErrorCodes.Exception };
        }
    }
}
