using FluentValidation;
using KafeApi.Application.Dtos.ReviewDtos;

namespace KafeApi.Application.Validators.Review
{
    public class CreateReviewValidator:AbstractValidator<CreateReviewDto>
    {
        public CreateReviewValidator()
        {
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Yorum boş olamaz") ;
        }
    }
}
