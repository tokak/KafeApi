using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.CategoryDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Services.Concrete;

public class CategoryServices : ICategoryServices
{
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
    private readonly IValidator<UpdateCategoryDto> _updateCategoryDto;
    public CategoryServices(IGenericRepository<Category> categoryRepository, IMapper mapper, IValidator<CreateCategoryDto> createCategoryValidator, IValidator<UpdateCategoryDto> updateCategoryDto)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _createCategoryValidator = createCategoryValidator;
        _updateCategoryDto = updateCategoryDto;
    }

    public async Task<ResponseDto<object>> AddCategory(CreateCategoryDto dto)
    {
        try
        {
            var validate = await _createCategoryValidator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = string.Join(" | ", validate.Errors.Select(x => x.ErrorMessage).ToList()),
                    ErrorCode = ErrorCodes.ValidationError,
                };
            }
            var category = _mapper.Map<Category>(dto);
            await _categoryRepository.AddAsync(category);
            return new ResponseDto<object> { Success = true, Data = category, Message = "Kategori Eklenildi." };
        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Data = null, Message = "Bir hata oluştu.", ErrorCode = ErrorCodes.Exception };
        }

    }

    public async Task<ResponseDto<object>> DeleteCategory(int id)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Kategori bulunamadı", ErrorCode = ErrorCodes.NotFound };
            }

            await _categoryRepository.DeleteAsync(category);
            return new ResponseDto<object> { Success = true, Data = category, Message = "Kategori silindi" };

        }
        catch (Exception ex)
        {
            return new ResponseDto<object>
            {
                Success = false,
                Message = "Bir Hata Oluştu.",
                ErrorCode = ErrorCodes.Exception
            };
        }

    }

    public async Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories()
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories.Count() == 0)
            {
                return new ResponseDto<List<ResultCategoryDto>> { Success = false, Message = "Kategori Bulunamadı.", ErrorCode = ErrorCodes.NotFound };

            }
            var result = _mapper.Map<List<ResultCategoryDto>>(categories);
            return new ResponseDto<List<ResultCategoryDto>> { Success = true, Data = result, };
        }
        catch (Exception ex)
        {
            return new ResponseDto<List<ResultCategoryDto>>
            {
                Success = false,
                Message = "Bir Hata Oluştu.",
                ErrorCode = ErrorCodes.Exception
            };
        }

    }

    public async Task<ResponseDto<DetailCategoryDto>> GetByIdCategory(int id)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return new ResponseDto<DetailCategoryDto> { Success = false, Message = "Kategori Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
            }
            var result = _mapper.Map<DetailCategoryDto>(category);
            return new ResponseDto<DetailCategoryDto> { Success = true, Data = result };
        }
        catch (Exception ex)
        {
            return new ResponseDto<DetailCategoryDto> { Success = false, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
        }

    }

    public async Task<ResponseDto<object>> UpdateCategory(UpdateCategoryDto dto)
    {

        try
        {
            var validate = await _updateCategoryDto.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = string.Join(" , ", validate.Errors.Select(x => x.ErrorMessage).ToList()),
                    Data = null,
                    ErrorCode = ErrorCodes.ValidationError
                };
            }
            var categoryfind = await _categoryRepository.GetByIdAsync(dto.Id);
            if (categoryfind == null)
            {
                return new ResponseDto<object> { Success = false, Message = "Kayıt bulunamadı.", Data = null, ErrorCode = ErrorCodes.NotFound };
            }
            var category = _mapper.Map(dto, categoryfind);
            await _categoryRepository.UpdateAsync(category);
            return new ResponseDto<object> { Success = true, Message = "Kayıt güncellendi.", Data = category };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object> { Success = false, Message = "Bir hata oluştu.", Data = null, ErrorCode = ErrorCodes.Exception };
        }
    }
}
