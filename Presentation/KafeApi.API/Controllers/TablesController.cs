using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.TableDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [Route("api/tables")]
    [ApiController]
    public class TablesController : BaseController
    {
        private readonly ITableServices _tableServices;

        public TablesController(ITableServices tableServices)
        {
            _tableServices = tableServices;
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAllTables()
        {
            var result = await _tableServices.GetAllTables();
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTable(int id)
        {
            var result = await _tableServices.GetByIdTable(id);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public async Task<IActionResult> AddTable(CreateTableDto dto)
        {
            var result = await _tableServices.AddTable(dto);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut]
        public async Task<IActionResult> UpdateTable(UpdateTableDto dto)
        {
            var result = await _tableServices.UpdateTable(dto);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableServices.DeleteTable(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("tablenumber")]
        public async Task<IActionResult> GetByTableNumber([FromQuery]int tableNumber)
        {
            var result = await _tableServices.GetByTableNumber(tableNumber);
            return CreateResponse(result);
        }
        //[Authorize(Roles = "Admin,Employee")]
        //[HttpGet("gisactivetables")]
        //public async Task<IActionResult> GetAllTablesIsActive()
        //{
        //    var result = await _tableServices.GetAllActiveTables();
        //    return CreateResponse(result);
        //}
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("statusbyid")]
        public async Task<IActionResult> UpdateTableStatusById(int id)
        {
            var result = await _tableServices.UpdateTableStatusById(id);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("statusbytablenumber")]
        public async Task<IActionResult> UpdateTableStatusByTableNumber(int Number)
        {
            var result = await _tableServices.UpdateTablesStatusByTableNumber(Number);
            return CreateResponse(result);
        }
    }
}
