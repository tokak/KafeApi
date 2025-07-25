using FluentValidation;
using KafeApi.Application.Dtos.ReviewDtos;

namespace KafeApi.Application.Validators.Review;

public class UpdateReviewValidator : AbstractValidator<UpdateReviewDto>
{
    public UpdateReviewValidator()
    {
        RuleFor(x => x.Comment).NotEmpty().WithMessage("Yorum boş olamaz") ;
    }
}
