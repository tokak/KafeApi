using FluentValidation;
using KafeApi.Application.Dtos.UserDtos;

namespace KafeApi.Application.Validators.User;

public class RegisterValidator:AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Ad alanı boş olamaz.").MinimumLength(2).WithMessage("Min 2 karekter olmalı");
        RuleFor(x=>x.Surname).NotEmpty().WithMessage("Soyad alanı boş olamaz.").MinimumLength(2).WithMessage("Min 2 karekter olmalı");
        RuleFor(x=>x.Email).NotEmpty().WithMessage("Email alanı boş olamaz.").EmailAddress().WithMessage("Geçersiz email adresi");
        RuleFor(x=>x.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz.").MinimumLength(6).WithMessage("Min 6 karekterden oluşmalı");

    }
}
