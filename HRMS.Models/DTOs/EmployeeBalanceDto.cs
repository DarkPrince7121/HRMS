namespace HRMS.Models.DTOs;

public class EmployeeBalanceDto
{
    public int EmployeeId { get; set; }
    public decimal AmountBalance { get; set; }
    public decimal PendingWithdrawals { get; set; }
    public decimal NetBalance { get; set; }
}
