using FluentValidation;
using KafeApi.Application.Dtos.CategoryDtos;

namespace KafeApi.Application.Validators.Category;

public class UpdateCategoryValitator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValitator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori adı boş olamaz").Length(3, 30).WithMessage("Kategori adı 3 ile 30 karekter arasında olmalıdır.");
    }
}
