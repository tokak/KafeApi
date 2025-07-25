using FluentValidation;
using KafeApi.Application.Dtos.CafeInfoDtos;

namespace KafeApi.Application.Validators.CafeInfo;

public class UpdateCafeInfoValidator:AbstractValidator<UpdateCafeInfoDto>
{
    public UpdateCafeInfoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Kafe adı boş olamaz");
    }
}
