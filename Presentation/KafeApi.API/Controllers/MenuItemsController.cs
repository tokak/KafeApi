using KafeApi.Application.Dtos.MenuItemDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [Route("api/menuitems")]
    [ApiController]
    public class MenuItemsController : BaseController
    {
        private readonly IMenuItemServices _menuItemServices;

        public MenuItemsController(IMenuItemServices menuItemServices)
        {
            _menuItemServices = menuItemServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems()
        {
            var result = await _menuItemServices.GetAllMenuItems();
            //if (!result.Success)
            //{
            //    if (result.ErrorCodes == ErrorCodes.NotFound)
            //    {
            //        return Ok(result);
            //    }
            //    return BadRequest(result);
            //}
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMenuItem(int id)
        {
            var result = await _menuItemServices.GetByIdMenuItem(id);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddMenuItem(CreateMenuItemDto dto)
        {
            var result = await _menuItemServices.AddMenuItem(dto);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            var result = await _menuItemServices.UpdateMenuItem(dto);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result = await _menuItemServices.DeleteMenuItem(id);
            return CreateResponse(result);
        }
    }
}
