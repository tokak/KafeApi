using KafeApi.Application.Dtos.CategoryDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController
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
        //if (!result.Success)
        //{
        //    if (result.ErrorCodes == ErrorCodes.NotFound)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}
        //return Ok(result);
        return CreateResponse(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdCategory(int id)
    {
        var result = await _categoryServices.GetByIdCategory(id);
        return CreateResponse(result); ;
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CreateCategoryDto dto)
    {
        var result = await _categoryServices.AddCategory(dto);
        return CreateResponse(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
    {
        var result = await _categoryServices.UpdateCategory(dto);
        return CreateResponse(result);
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _categoryServices.DeleteCategory(id);
        return CreateResponse(result);
    }
    [HttpGet("getallcategorieswithmenuitems")]
    public async Task<IActionResult> GetAllCategoriesWithMenuItems()
    {
        var result = await _categoryServices.GetCategoriesWithMenuItem();
        return CreateResponse(result);
    }
}
