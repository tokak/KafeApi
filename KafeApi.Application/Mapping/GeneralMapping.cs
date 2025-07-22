using AutoMapper;
using KafeApi.Application.Dtos.CategoryDtos;
using KafeApi.Application.Dtos.MenuItemDtos;
using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Application.Dtos.TableDtos;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, ResultCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        CreateMap<Category, DetailCategoryDto>().ReverseMap();

        CreateMap<MenuItem, CreateMenuItemDto>().ReverseMap();
        CreateMap<MenuItem, ResultMenuItemDto>().ReverseMap();
        CreateMap<MenuItem, UpdateMenuItemDto>().ReverseMap();
        CreateMap<MenuItem, DetailMenuItemDto>().ReverseMap();

        CreateMap<Table, CreateTableDto>().ReverseMap();
        CreateMap<Table, ResultTableDto>().ReverseMap();
        CreateMap<Table, UpdateTableDto>().ReverseMap();
        CreateMap<Table, DetailTableDto>().ReverseMap();

        CreateMap<OrderItem, CreateOrderItemDto>().ReverseMap();
        CreateMap<OrderItem, ResultOrderItemDto>().ReverseMap();
        CreateMap<OrderItem, UpdateOrderItemDto>().ReverseMap();
        CreateMap<OrderItem, DetailOrderItemDto>().ReverseMap();
    }

}
