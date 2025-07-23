using FluentValidation;
using KafeApi.Application.Dtos.OrderDtos;

namespace KafeApi.Application.Validators.Order;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
{
    public UpdateOrderValidator()
    {
        //RuleFor(x => x.TotalPrice).NotEmpty().WithMessage("Toplam tutar boş olamaz").GreaterThan(0).WithMessage("Toplam tutat 0 dan büyük olmalıdır.");
    }
}
