﻿namespace KafeApi.Application.Dtos.TableDtos;

public class UpdateTableDto
{
    public int Id { get; set; }
    public string TableCode { get; set; }
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
}
