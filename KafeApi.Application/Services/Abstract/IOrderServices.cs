using KafeApi.Application.Dtos.OrderDtos;
using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Application.Dtos.ResponseDtos;

namespace KafeApi.Application.Services.Abstract;

public interface IOrderServices
{
    Task<ResponseDto<List<ResulOrderDto>>> GetAllOrders();
    Task<ResponseDto<DetailOrderDto>> GetByOrderId(int id);
    Task<ResponseDto<object>> AddOrder(CreateOrderDto dto);
    Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto);
    Task<ResponseDto<object>> DeleteOrder(int id);
    Task<ResponseDto<List<ResulOrderDto>>> GetAllOrdersWithDetail();
    Task<ResponseDto<object>> UpdateOrderStatusHazir(int orderId);
    Task<ResponseDto<object>> UpdateOrderStatusIptalEdildi(int orderId);
    Task<ResponseDto<object>> UpdateOrderStatusTeslimEdildi(int orderId);
    Task<ResponseDto<object>> UpdateOrderStatusIptalOdendi(int orderId);
    Task<ResponseDto<object>> AddOrderItemByOrderId(AddOrderItemByOrderDto dto);

}
