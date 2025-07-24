using KafeApi.Application.Dtos.CategoryDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController : BaseController
{
    private readonly ICategoryServices _categoryServices;
    private readonly Serilog.ILogger _logger;
    public CategoriesController(ICategoryServices categoryServices, Serilog.ILogger logger)
    {
        _categoryServices = categoryServices;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        _logger.Information("get-categories");
        var result = await _categoryServices.GetAllCategories();
        //if (!result.Success)
        //{
        //    if (result.ErrorCodes == ErrorCodes.NotFound)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}
        //return Ok(result);
        _logger.Information("get-categories: " + result.Success);
        _logger.Warning("get-categories: " + result.Success);
        _logger.Error("get-categories: " + result.Success);
        _logger.Debug("get-categories: " + result.Success);

        return CreateResponse(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdCategory(int id)
    {
        var result = await _categoryServices.GetByIdCategory(id);
        return CreateResponse(result); ;
    }
    [Authorize(Roles ="Admin")]
    [HttpPost]
    public async Task<IActionResult> AddCategory(CreateCategoryDto dto)
    {
        var result = await _categoryServices.AddCategory(dto);
        return CreateResponse(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
    {
        var result = await _categoryServices.UpdateCategory(dto);
        return CreateResponse(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _categoryServices.DeleteCategory(id);
        return CreateResponse(result);
    }
    [HttpGet("withmenuitems")]
    public async Task<IActionResult> GetAllCategoriesWithMenuItems()
    {
        var result = await _categoryServices.GetCategoriesWithMenuItem();
        return CreateResponse(result);
    }
}
