﻿using KafeApi.Domain.Entities;

namespace KafeApi.Application.Dtos.ReviewDtos;

public class CreateReviewDto
{
    public string UserId { get; set; }
    public int CafeId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; } 
    public DateTime CreatedAt { get; set; }
}
