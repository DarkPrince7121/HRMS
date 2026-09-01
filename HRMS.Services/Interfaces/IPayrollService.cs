using HRMS.Models.Entities;

namespace HRMS.Services.Interfaces;

public interface IPayrollService
{
    Task<IEnumerable<Payroll>> GetAllAsync();
    Task GeneratePayrollForMonthAsync(DateTime month);
    Task<decimal> CalculateNetPayAsync(int employeeId, DateTime month);
    Task<(decimal BasicPay, decimal Allowances, decimal LeaveDeductions, decimal AbsentDeductions)> GetPayrollBreakdownAsync(int employeeId, DateTime month, decimal baseSalary);
}