using System.ComponentModel.DataAnnotations;

namespace HRMS.Models.DTOs;

public class EmployeeRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Base salary must be a non-negative value.")]
    public decimal BaseSalary { get; set; }
    public int? UserId { get; set; }
}