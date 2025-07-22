using KafeApi.Application.Dtos.OrderDtos;
using KafeApi.Application.Dtos.ResponseDtos;

namespace KafeApi.Application.Services.Abstract;

public interface IOrderServices
{
    Task<ResponseDto<List<ResulOrderDto>>> GetAllOrders();
    Task<ResponseDto<DetailOrderDto>> GetByOrderId(int id);
    Task<ResponseDto<object>> AddOrder(CreateOrderDto dto);
    Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto);
    Task<ResponseDto<object>> DeleteOrder(int id);
}
