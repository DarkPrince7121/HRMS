using System;

namespace HRMS.Models.Entities;

public class Attendance
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Remarks { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}