using FluentValidation;
using KafeApi.Application.Dtos.CategoryDtos;

namespace KafeApi.Application.Validators.Category;

public class AddCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public AddCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori adı boş olamaz.").Length(3, 30).WithMessage("Kategori adı 3 ile 30 karakter arasında olmalıdır.");
    }

}
