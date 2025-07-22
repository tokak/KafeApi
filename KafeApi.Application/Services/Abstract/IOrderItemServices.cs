using KafeApi.Application.Dtos.CategoryDtos;
using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Application.Dtos.ResponseDtos;

namespace KafeApi.Application.Services.Abstract;

public interface IOrderItemServices
{
    Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems();
    Task<ResponseDto<DetailOrderItemDto>> GetByOrderItemId(int id);
    Task<ResponseDto<object>> AddOrderItem(CreateOrderItemDto dto);
    Task<ResponseDto<object>> UpdateOrderItem(UpdateOrderItemDto dto);
    Task<ResponseDto<object>> DeleteOrderItem(int id);
}
