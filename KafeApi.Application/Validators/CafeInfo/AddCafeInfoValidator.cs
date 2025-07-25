using FluentValidation;
using KafeApi.Application.Dtos.CafeInfoDtos;

namespace KafeApi.Application.Validators.CafeInfo;

public class AddCafeInfoValidator : AbstractValidator<CreateCafeInfoDto>
{
    public AddCafeInfoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Cafe adı boş olamaz");
    }
}
