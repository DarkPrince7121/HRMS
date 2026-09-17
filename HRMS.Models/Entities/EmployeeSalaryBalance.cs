namespace HRMS.Models.Entities;

public class EmployeeSalaryBalance
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public decimal AmountBalance { get; set; }
}
