namespace HRMS.Models.DTOs;

public class EmployeeApiDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public decimal BaseSalary { get; set; }
}
