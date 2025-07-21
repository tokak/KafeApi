﻿using KafeApi.Application.Dtos.CategoryDtos;
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
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdCategory(int id)
    {
        var result = await _categoryServices.GetByIdCategory(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CreateCategoryDto dto)
    {
        await _categoryServices.AddCategory(dto);
        return Ok("Kategori Oluşturuldu.");
    }
    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
    {
        await _categoryServices.UpdateCategory(dto);
        return Ok("Kategori güncellendi.");
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryServices.DeleteCategory(id);
        return Ok("Kategori silindi.");
    }
}
