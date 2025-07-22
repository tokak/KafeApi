using FluentValidation;
using KafeApi.Application.Dtos.MenuItemDtos;

namespace KafeApi.Application.Validators.MenuItem;

public class CreateMenuItemValidator : AbstractValidator<CreateMenuItemDto>
{
    public CreateMenuItemValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Menü item adı boş olamaz").Length(2, 40).WithMessage("Menu item 2 ile 40 karekter arasında olmalı");
        RuleFor(x => x.Desription).NotEmpty().WithMessage("Menü item açıklama boş olamaz").Length(3, 100).WithMessage("Menu item 3 ile 100 karekter arasında olmalı");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Menü item fiyat boş olamaz").GreaterThan(0).WithMessage("Menu item fiyatı 0 dan büyük olmalı");
        RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Menü item fotoğraf boş olamaz");
    }
}
