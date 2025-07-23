using KafeApi.Application.Dtos.CategoryDtos;
using KafeApi.Application.Dtos.ResponseDtos;

namespace KafeApi.Application.Services.Abstract;

public interface ICategoryServices
{
    Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories();
    Task<ResponseDto<DetailCategoryDto>> GetByIdCategory(int id);
    Task<ResponseDto<object>> AddCategory(CreateCategoryDto dto);
    Task<ResponseDto<object>> UpdateCategory(UpdateCategoryDto dto);
    Task<ResponseDto<object>> DeleteCategory(int id);
    Task<ResponseDto<List<ResultCategoriesWithMenuDto>>> GetCategoriesWithMenuItem();
}
