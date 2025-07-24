//using KafeApi.Application.Dtos.CategoryDtos;
//using KafeApi.Application.Dtos.OrderItemDtos;
//using KafeApi.Application.Services.Abstract;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace KafeApi.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrderItemsController : BaseController
//    {
//        private readonly IOrderItemServices _orderItemServices;

//        public OrderItemsController(IOrderItemServices orderItemServices)
//        {
//            _orderItemServices = orderItemServices;
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetAllOrderItems()
//        {
//            var result = await _orderItemServices.GetAllOrderItems();            
//            return CreateResponse(result);
//        }
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetByIdOrderItem(int id)
//        {
//            var result = await _orderItemServices.GetByOrderItemId(id);
//            return CreateResponse(result); ;
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddOrderItem(CreateOrderItemDto dto)
//        {
//            var result = await _orderItemServices.AddOrderItem(dto);
//            return CreateResponse(result);
//        }
//        [HttpPut]
//        public async Task<IActionResult> UpdateOrderItem(UpdateOrderItemDto dto)
//        {
//            var result = await _orderItemServices.UpdateOrderItem(dto);
//            return CreateResponse(result);
//        }
//        [HttpDelete]
//        public async Task<IActionResult> DeleteOrderItem(int id)
//        {
//            var result = await _orderItemServices.DeleteOrderItem(id);
//            return CreateResponse(result);
//        }
//    }
//}
