using System;

namespace HRMS.Models.DTOs;

public class WithdrawalApplicationDto
{
    public int Id { get; set; }
    public int StatusId { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public int CreatedPersonId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastChangeDate { get; set; }
    public DateTime StatusLastChangeDate { get; set; }
    public decimal Amount { get; set; }
    public int? SelectedEmployeeId { get; set; }
    public string SelectedEmployeeName { get; set; } = string.Empty;
    public string SelectedEmployeeEmail { get; set; } = string.Empty;
}
