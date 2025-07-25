using KafeApi.Domain.Entities;

namespace KafeApi.Application.Dtos.ReviewDtos;

public class UpdateReviewDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int CafeId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; } //1-5 arasında bir değer validator eklenecek
    public DateTime CreatedAt { get; set; }
}
