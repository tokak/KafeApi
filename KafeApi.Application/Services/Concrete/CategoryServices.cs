using KafeApi.Application.Dtos.CategoryDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Services.Concrete;

public class CategoryServices : ICategoryServices
{
    private readonly IGenericRepository<Category> _categoryRepository;

    public CategoryServices(IGenericRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Task AddCategory(CreateCategoryDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ResultCategoryDto>> GetAllCategories()
    {
        throw new NotImplementedException();
    }

    public Task<DetailCategoryDto> GetByIdCategory(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCategory(UpdateCategoryDto dto)
    {
        throw new NotImplementedException();
    }
}
