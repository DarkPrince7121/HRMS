namespace HRMS.Models.DTOs;

public class EmployeeRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public decimal BaseSalary { get; set; }
    public int? UserId { get; set; }
}