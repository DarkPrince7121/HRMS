namespace HRMS.Models.DTOs;

public class UpdateStatusDto
{
    public int StatusId { get; set; }
    public decimal? Amount { get; set; }
    public int? SelectedEmployeeId { get; set; }
}
