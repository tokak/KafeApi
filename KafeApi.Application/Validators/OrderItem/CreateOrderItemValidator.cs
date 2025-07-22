﻿using FluentValidation;
using KafeApi.Application.Dtos.OrderItemDtos;

namespace KafeApi.Application.Validators.OrderItem;

public class CreateOrderItemValidator:AbstractValidator<CreateOrderItemDto>
{
    public CreateOrderItemValidator()
    { 
        RuleFor(x => x.Quantity).NotEmpty().WithMessage("Sipariş adeti boş olamaz.").GreaterThan(0).WithMessage("Sipariş adeti  0'dan büyük olmalıdır.");
    }
}
