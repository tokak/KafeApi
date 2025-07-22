

using FluentValidation;
using KafeApi.Application.Dtos.OrderItemDtos;

namespace KafeApi.Application.Validators.OrderItem;

public class UpdateOrderItemValidator : AbstractValidator<UpdateOrderItemDto>
{
    public UpdateOrderItemValidator()
    {
        RuleFor(x => x.Quantity).NotEmpty().WithMessage("Adet boş olamaz.").GreaterThan(0).WithMessage("Adet 0'dan büyük olmalıdır.");
    }
}
