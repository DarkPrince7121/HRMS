namespace HRMS.Models.DTOs;

public class PayrollResponse
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime PayDate { get; set; }
    public decimal GrossPay { get; set; }
    public decimal Deductions { get; set; }
    public decimal NetPay { get; set; }
    public decimal BasicPay { get; set; }
    public decimal Allowances { get; set; }
    public decimal LeaveDeductions { get; set; }
    public decimal AbsentDeductions { get; set; }
    public string Status { get; set; } = string.Empty;
}