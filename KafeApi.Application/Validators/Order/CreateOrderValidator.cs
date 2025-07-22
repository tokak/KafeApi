using FluentValidation;
using KafeApi.Application.Dtos.OrderDtos;

namespace KafeApi.Application.Validators.Order;

public class CreateOrderValidator:AbstractValidator<CreateOrderDto>
{
    public CreateOrderValidator()
    {
        RuleFor(x=>x.TotalPrice).NotEmpty().WithMessage("Toplam tutar boş olamaz").GreaterThan(0).WithMessage("Toplam tutat 0 dan büyük olmalıdır.");
    }
}
