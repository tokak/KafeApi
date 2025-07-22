﻿namespace KafeApi.Domain.Entities;

public class Table
{
    public int Id { get; set; }
    public string TableCode { get; set; }
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
}
