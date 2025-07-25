using KafeApi.Application.Dtos.CafeInfoDtos;
using KafeApi.Application.Dtos.CategoryDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeInfoController : BaseController
    {
        private readonly ICafeInfoServices _cafeInfoServices;
        private readonly Serilog.ILogger _logger;

        public CafeInfoController(ICafeInfoServices cafeInfoServices, Serilog.ILogger logger)
        {
            _cafeInfoServices = cafeInfoServices;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCafeInfos()
        {
            _logger.Information("get-cafeinfo");
            var result = await _cafeInfoServices.GetAllCafeInfos();           
            _logger.Information("get-categories: " + result.Success);
            _logger.Warning("get-categories: " + result.Success);
            _logger.Error("get-categories: " + result.Success);
            _logger.Debug("get-categories: " + result.Success);

            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCafeInfo([FromRoute] int id)
        {
            var result = await _cafeInfoServices.GetByIdCafeInfo(id);
            return CreateResponse(result); ;
        }
   
        [HttpPost]
        public async Task<IActionResult> AddCafeInfo(CreateCafeInfoDto dto)
        {
            var result = await _cafeInfoServices.AddCafeInfo(dto);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCafeInfo(UpdateCafeInfoDto dto)
        {
            var result = await _cafeInfoServices.UpdateCafeInfo(dto);
            return CreateResponse(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCafeInfo(int id)
        {
            var result = await _cafeInfoServices.DeleteCafeInfo(id);
            return CreateResponse(result);
        }
       
    }
}
