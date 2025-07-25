using AutoMapper;
using KafeApi.Application.Dtos.CafeInfoDtos;
using KafeApi.Application.Dtos.CategoryDtos;
using KafeApi.Application.Dtos.MenuItemDtos;
using KafeApi.Application.Dtos.OrderDtos;
using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Application.Dtos.ReviewDtos;
using KafeApi.Application.Dtos.TableDtos;
using KafeApi.Application.Dtos.UserDtos;
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
        CreateMap<Category, ResultCategoriesWithMenuDto>().ReverseMap();

        CreateMap<MenuItem, CreateMenuItemDto>().ReverseMap();
        CreateMap<MenuItem, ResultMenuItemDto>().ReverseMap();
        CreateMap<MenuItem, UpdateMenuItemDto>().ReverseMap();
        CreateMap<MenuItem, DetailMenuItemDto>().ReverseMap();
        CreateMap<MenuItem, CategoriesMenuItemDto>().ReverseMap();

        CreateMap<Table, CreateTableDto>().ReverseMap();
        CreateMap<Table, ResultTableDto>().ReverseMap();
        CreateMap<Table, UpdateTableDto>().ReverseMap();
        CreateMap<Table, DetailTableDto>().ReverseMap();

        CreateMap<OrderItem, CreateOrderItemDto>().ReverseMap();
        CreateMap<OrderItem, ResultOrderItemDto>().ReverseMap();
        CreateMap<OrderItem, UpdateOrderItemDto>().ReverseMap();
        CreateMap<OrderItem, DetailOrderItemDto>().ReverseMap();

        CreateMap<Order, CreateOrderDto>().ReverseMap();
        CreateMap<Order, ResulOrderDto>().ReverseMap();
        CreateMap<Order, UpdateOrderDto>().ReverseMap();
        CreateMap<Order, DetailOrderDto>().ReverseMap();


        CreateMap<CafeInfo, CreateCafeInfoDto>().ReverseMap();
        CreateMap<CafeInfo, ResultCafeInfoDto>().ReverseMap();
        CreateMap<CafeInfo, UpdateCafeInfoDto>().ReverseMap();
        CreateMap<CafeInfo, DetailCafeInfoDto>().ReverseMap();


        CreateMap<Review, CreateReviewDto>().ReverseMap();
        CreateMap<Review, ResultReviewDto>().ReverseMap();
        CreateMap<Review, UpdateReviewDto>().ReverseMap();
        CreateMap<Review, DetailReviewDto>().ReverseMap();

        //CreateMap< RegisterDto>().ReverseMap();

    }

}
