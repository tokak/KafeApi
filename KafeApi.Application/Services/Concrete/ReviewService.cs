using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.ReviewDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;

namespace KafeApi.Application.Services.Concrete;

public class ReviewService : IReviewService
{
    private readonly IGenericRepository<Review> _reviewRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateReviewDto> _createReviewValidator;
    private readonly IValidator<UpdateReviewDto> _updateReviewValidator;

    public ReviewService(IMapper mapper, IValidator<CreateReviewDto> createReviewValidator, IValidator<UpdateReviewDto> updateReviewValidator, IGenericRepository<Review> reviewRepository)
    {
        _mapper = mapper;
        _createReviewValidator = createReviewValidator;
        _updateReviewValidator = updateReviewValidator;
        _reviewRepository = reviewRepository;
    }

    public async Task<ResponseDto<object>> AddReview(CreateReviewDto createReviewDto)
    {
        try
        {
            var validationResult = await _createReviewValidator.ValidateAsync(createReviewDto);
            if (!validationResult.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage).ToList()),
                    ErrorCode = ErrorCodes.ValidationError,

                };
            }

            var review = _mapper.Map<Review>(createReviewDto);
            review.CreatedAt = DateTime.Now;
            await _reviewRepository.AddAsync(review);
            return new ResponseDto<object>
            {
                Success = true,
                Data = review,
                Message = "Yorum başarıyla eklendi.",
            };

        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Data = null, Message = "bir hata oluştu.", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<List<ResultReviewDto>>> GetAllReviews()
    {
        try
        {
            var reviews = await _reviewRepository.GetAllAsync();
            if (reviews == null || !reviews.Any())
            {
                return new ResponseDto<List<ResultReviewDto>>
                { Success = false, Data = null, Message = "Yorum bulunamadı.", ErrorCode = ErrorCodes.NotFound };
            }

            var resultReviews = _mapper.Map<List<ResultReviewDto>>(reviews);
            return new ResponseDto<List<ResultReviewDto>>
            { Success = true, Data = resultReviews, Message = "Yorumlar başarıyla getirildi." };
        }
        catch (Exception)
        {

            return new ResponseDto<List<ResultReviewDto>>
            { Success = false, Data = null, Message = "bir hata oluştu.", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<DetailReviewDto>> GetByIdReview(int id)
    {
        try
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null)
            {
                return new ResponseDto<DetailReviewDto>
                { Success = false, Data = null, Message = "Yorum bulunamadı.", ErrorCode = ErrorCodes.NotFound };
            }
            var detailReview = _mapper.Map<DetailReviewDto>(review);
            return new ResponseDto<DetailReviewDto>
            { Success = true, Data = detailReview, Message = "Yorum başarıyla getirildi." };
        }
        catch (Exception)
        {

            return new ResponseDto<DetailReviewDto>
            { Success = false, Data = null, Message = "bir hata oluştu.", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> DeleteReview(int id)
    {
        try
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Yorum bulunamadı.", ErrorCode = ErrorCodes.NotFound };
            }
            await _reviewRepository.DeleteAsync(review);
            return new ResponseDto<object> { Success = true, Data = null, Message = "Yorum başarıyla silindi." };

        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Data = null, Message = "bir hata oluştu.", ErrorCode = ErrorCodes.Exception };
        }
    }

    public async Task<ResponseDto<object>> UpdateReview(UpdateReviewDto updateReviewDto)
    {
        try
        {
            var validationResult = await _updateReviewValidator.ValidateAsync(updateReviewDto);
            if (!validationResult.IsValid)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage).ToList()),
                    ErrorCode = ErrorCodes.ValidationError,
                };
            }
            var review = await _reviewRepository.GetByIdAsync(updateReviewDto.Id);
            if (review == null)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Yorum bulunamadı.", ErrorCode = ErrorCodes.NotFound };
            }
            _mapper.Map(updateReviewDto, review);
            await _reviewRepository.UpdateAsync(review);
            return new ResponseDto<object> { Success = true, Data = review, Message = "Yorum başarıyla güncellendi." };

        }
        catch (Exception)
        {
            return new ResponseDto<object> { Success = false, Data = null, Message = "bir hata oluştu.", ErrorCode = ErrorCodes.Exception };
        }
    }
}
