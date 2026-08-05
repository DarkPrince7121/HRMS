using System;

namespace HRMS.Models.DTOs;

public class HolidayResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public bool IsRecurring { get; set; }
}