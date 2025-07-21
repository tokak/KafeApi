﻿using KafeApi.Application.Dtos.CategoryDtos;

namespace KafeApi.Application.Services.Abstract;

public interface ICategoryServices
{
    Task<List<ResultCategoryDto>> GetAllCategories();
    Task<DetailCategoryDto> GetByIdCategory(int id);
    Task AddCategory(CreateCategoryDto dto);
    Task UpdateCategory(UpdateCategoryDto dto);
    Task DeleteCategory(int id);
}
