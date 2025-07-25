
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.ReviewDtos;

namespace KafeApi.Application.Services.Abstract;

public interface IReviewService
{
    Task<ResponseDto<List<ResultReviewDto>>> GetAllReviews();
    Task<ResponseDto<DetailReviewDto>> GetByIdReview(int id);
    Task<ResponseDto<object>> AddReview(CreateReviewDto dto);
    Task<ResponseDto<object>> UpdateReview(UpdateReviewDto dto);
    Task<ResponseDto<object>> DeleteReview(int id);
}
