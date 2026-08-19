using System;

namespace HRMS.Models.DTOs;

public class AttendanceRequest
{
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Remarks { get; set; }
}