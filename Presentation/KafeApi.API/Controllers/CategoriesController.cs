using KafeApi.Application.Dtos.CategoryDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryServices _categoryServices;
    public CategoriesController(ICategoryServices categoryServices)
    {
        _categoryServices = categoryServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _categoryServices.GetAllCategories();
        if (!result.Success)
        {
            if (result.ErrorCodes == ErrorCodes.NotFound)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdCategory(int id)
    {
        var result = await _categoryServices.GetByIdCategory(id);
        if (!result.Success)
        {
            if (result.ErrorCodes == ErrorCodes.NotFound)
            {
                return Ok();
            }
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CreateCategoryDto dto)
    {
        var result = await _categoryServices.AddCategory(dto);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
    {
        var result = await _categoryServices.UpdateCategory(dto);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _categoryServices.DeleteCategory(id);
        if (!result.Success)
        {
            if (result.ErrorCodes == ErrorCodes.NotFound)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        return Ok(result);

    }
}
