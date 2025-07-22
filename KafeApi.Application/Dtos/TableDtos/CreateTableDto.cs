namespace KafeApi.Application.Dtos.TableDtos;

public class CreateTableDto
{
    public string TableCode { get; set; }
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
}
