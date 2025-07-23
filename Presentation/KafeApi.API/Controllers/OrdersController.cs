using KafeApi.Application.Dtos.OrderDtos;
using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IOrderServices _orderServices;

        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderServices.GetAllOrders();
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrder(int id)
        {
            var result = await _orderServices.GetByOrderId(id);
            return CreateResponse(result); ;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderDto dto)
        {
            var result = await _orderServices.AddOrder(dto);
            return CreateResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            var result = await _orderServices.UpdateOrder(dto);
            return CreateResponse(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderServices.DeleteOrder(id);
            return CreateResponse(result);
        }
        [HttpGet("getallorderswithdetail")]
        public async Task<IActionResult> GetAllOrdersWithDetail()
        {
            var result = await _orderServices.GetAllOrdersWithDetail();
            return CreateResponse(result);
        }

        [HttpPut("updateorderstatushazir")]
        public async Task<IActionResult> UpdateOrderStatusHazir(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusHazir(orderId);
            return CreateResponse(result);
        }
        [HttpPut("updateorderstatusiptal")]
        public async Task<IActionResult> UpdateOrderStatusIptal(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusIptalEdildi(orderId);
            return CreateResponse(result);
        }
        [HttpPut("updateorderstatusteslim")]
        public async Task<IActionResult> UpdateOrderStatusTeslim(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusTeslimEdildi(orderId);
            return CreateResponse(result);
        }   
        [HttpPut("updateorderstatusodendi")]
        public async Task<IActionResult> UpdateOrderStatusOdendi(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusIptalOdendi(orderId);
            return CreateResponse(result);
        }

        //[HttpPut("addorderitembyorder")]
        //public async Task<IActionResult> UpdateOrderStatusTeslim(AddOrderItemByOrderDto dto)
        //{
        //    var result = await _orderServices.AddOrderItemByOrderId(dto);
        //    return CreateResponse(result);
        //}

    }
}
