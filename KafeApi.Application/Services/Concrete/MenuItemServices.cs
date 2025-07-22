using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.MenuItemDtos;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace KafeApi.Application.Services.Concrete;

public class MenuItemServices : IMenuItemServices
{
    private readonly IGenericRepository<MenuItem> _menuItemRepository;
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateMenuItemDto> _createMenuItemValidator;
    private readonly IValidator<UpdateMenuItemDto> _updateMenuItemValidator;
    public MenuItemServices(IGenericRepository<MenuItem> menuItemRepository, IMapper mapper, IValidator<CreateMenuItemDto> createMenuItemValidator, IValidator<UpdateMenuItemDto> updateMenuItemValidator, IGenericRepository<Category> categoryRepository)
    {
        _menuItemRepository = menuItemRepository;
        _mapper = mapper;
        _createMenuItemValidator = createMenuItemValidator;
        _updateMenuItemValidator = updateMenuItemValidator;
        _categoryRepository = categoryRepository;
    }

    public async Task<ResponseDto<object>> AddMenuItem(CreateMenuItemDto dto)
    {
        try
        {
            var valide = await _createMenuItemValidator.ValidateAsync(dto);
            if (!valide.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.ValidationError,
                    Message = string.Join(",", valide.Errors.Select(x => x.ErrorMessage).ToList())
                };
            }
            var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (checkCategory == null)
            {
                return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Eklemek istediğiniz kategori bulunamadı." };
            }
            var addMenuItem = _mapper.Map<MenuItem>(dto);
            await _menuItemRepository.AddAsync(addMenuItem);
            return new ResponseDto<object> { Success = true, Data = addMenuItem, Message = "Menü item eklenildi" };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object>
            {
                Success = false,
                Data = null,
                Message = "Bir hata oluştur.",
                ErrorCode = ErrorCodes.Exception
            };
        }
    }

    public async Task<ResponseDto<object>> DeleteMenuItem(int id)
    {
        try
        {
            var removeMenuItem = await _menuItemRepository.GetByIdAsync(id);
            if (removeMenuItem == null)
            {
                return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kayıt bulunamadı." };
            }
            await _menuItemRepository.DeleteAsync(removeMenuItem);
            return new ResponseDto<object> { Success = true, Data = removeMenuItem, Message = "Kayıt silindi." };

        }
        catch (Exception ex)
        {
            return new ResponseDto<object>
            {
                Success = false,
                Data = null,
                Message = "Bir hata oluştur.",
                ErrorCode = ErrorCodes.Exception
            };
        }

    }

    public async Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems()
    {
        try
        {
            var menuItems = await _menuItemRepository.GetAllAsync();
            var category = await _categoryRepository.GetAllAsync();
            var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
            if (result.Count() == 0)
            {
                return new ResponseDto<List<ResultMenuItemDto>>
                {
                    Success = true,
                    Data = result,
                    ErrorCode = ErrorCodes.NotFound,
                    Message = "Listelenecek kayıt bulunamadı."
                };
            }
            return new ResponseDto<List<ResultMenuItemDto>> { Success = true, Data = result };
        }
        catch (Exception ex)
        {
            return new ResponseDto<List<ResultMenuItemDto>>
            {
                Success = false,
                Data = null,
                ErrorCode = ErrorCodes.Exception,
                Message = "Bir hata oluştu."
            };
        }

    }

    public async Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id)
    {
        try
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            var categoryfind = await _categoryRepository.GetByIdAsync(menuItem.CategoryId);
            if (menuItem == null || categoryfind == null)
            {
                return new ResponseDto<DetailMenuItemDto> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kayıt  veya kategori bulunamadı." };
            }

            var result = _mapper.Map<DetailMenuItemDto>(menuItem);
            return new ResponseDto<DetailMenuItemDto> { Success = true, Data = result };
        }
        catch (Exception)
        {
            return new ResponseDto<DetailMenuItemDto>
            {
                Success = false,
                Data = null,
                ErrorCode = ErrorCodes.Exception,
                Message = "Bir hata oluştu."
            };
        }
    }

    public async Task<ResponseDto<object>> UpdateMenuItem(UpdateMenuItemDto dto)
    {
        try
        {
            var validate = await _updateMenuItemValidator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = string.Join(" , ", validate.Errors.Select(x => x.ErrorMessage).ToList()),
                    ErrorCode = ErrorCodes.ValidationError
                };
            }
            var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (checkCategory == null)
            {
                return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Eklemek istediğiniz kategori bulunamadı." };
            }
            var menuItem = _mapper.Map<MenuItem>(dto);
            await _menuItemRepository.UpdateAsync(menuItem);
            return new ResponseDto<object> { Success = true, Data = dto, Message = "Menü item ürünü güncellendi" };

        }
        catch (Exception ex)
        {
            return new ResponseDto<object>
            {
                Success = false,
                Data = null,
                Message = "Bir hata oluştu",
                ErrorCode = ErrorCodes.Exception
            };
        }

    }
}
