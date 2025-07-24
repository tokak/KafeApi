using KafeApi.Application.Dtos.OrderDtos;
using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IOrderServices _orderServices;

        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderServices.GetAllOrders();
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrder(int id)
        {
            var result = await _orderServices.GetByOrderId(id);
            return CreateResponse(result); ;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderDto dto)
        {
            var result = await _orderServices.AddOrder(dto);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            var result = await _orderServices.UpdateOrder(dto);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderServices.DeleteOrder(id);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("withdetail")]
        public async Task<IActionResult> GetAllOrdersWithDetail()
        {
            var result = await _orderServices.GetAllOrdersWithDetail();
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("statushazir")]
        public async Task<IActionResult> UpdateOrderStatusHazir(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusHazir(orderId);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("updateorderstatusiptal")]
        public async Task<IActionResult> UpdateOrderStatusIptal(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusIptalEdildi(orderId);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("tatusteslim")]
        public async Task<IActionResult> UpdateOrderStatusTeslim(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusTeslimEdildi(orderId);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("statusodendi")]
        public async Task<IActionResult> UpdateOrderStatusOdendi(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusIptalOdendi(orderId);
            return CreateResponse(result);
        }
    }
}
