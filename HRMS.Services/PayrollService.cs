using HRMS.Data;
using HRMS.Models.Entities;
using HRMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services;

public class PayrollService : IPayrollService
{
    private readonly ApplicationDbContext _context;

    public PayrollService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Payroll>> GetAllAsync()
    {
        return await _context.Payrolls
            .Include(p => p.Employee)
            .OrderByDescending(p => p.PayDate)
            .ToListAsync();
    }

    public async Task GeneratePayrollForMonthAsync(DateTime month)
    {
        var employees = await _context.Employees.ToListAsync();
        var firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        // Check if payroll already exists for this month for any employee to avoid duplicates
        // Simplified: delete existing for the month and regenerate
        var existing = await _context.Payrolls
            .Where(p => p.PayDate >= firstDayOfMonth && p.PayDate <= lastDayOfMonth)
            .ToListAsync();
        
        _context.Payrolls.RemoveRange(existing);

        foreach (var employee in employees)
        { 
            var breakdown = await GetPayrollBreakdownAsync(employee.Id, month, employee.BaseSalary);
            var deductions = breakdown.LeaveDeductions + breakdown.AbsentDeductions;
            var netPay = Math.Max(0, employee.BaseSalary - deductions);

            var payroll = new Payroll
            { 
                EmployeeId = employee.Id,
                PayDate = lastDayOfMonth,
                GrossPay = employee.BaseSalary,
                Deductions = deductions,
                NetPay = netPay,
                Status = "Processed"
            };

            _context.Payrolls.Add(payroll);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<decimal> CalculateNetPayAsync(int employeeId, DateTime month)
    {
        var employee = await _context.Employees.FindAsync(employeeId);
        if (employee == null) return 0;

        var breakdown = await GetPayrollBreakdownAsync(employeeId, month, employee.BaseSalary);
        decimal deductions = breakdown.LeaveDeductions + breakdown.AbsentDeductions;
        return Math.Max(0, employee.BaseSalary - deductions);
    }

    public async Task<(decimal BasicPay, decimal Allowances, decimal LeaveDeductions, decimal AbsentDeductions)> GetPayrollBreakdownAsync(int employeeId, DateTime month, decimal baseSalary)
    {
        var firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        // Account for Leave Management
        var leaves = await _context.Leaves
            .Where(l => l.EmployeeId == employeeId && l.Status == "Approved" &&
                        ((l.StartDate >= firstDayOfMonth && l.StartDate <= lastDayOfMonth) ||
                         (l.EndDate >= firstDayOfMonth && l.EndDate <= lastDayOfMonth)))
            .ToListAsync();

        int leaveDays = 0;
        foreach (var leave in leaves)
        { 
            // Deduct for approved leaves unless they are sick, maternity, or paternity
            if (leave.LeaveType != null &&
                !(leave.LeaveType.Contains("sick", StringComparison.OrdinalIgnoreCase) ||
                  leave.LeaveType.Contains("maternity", StringComparison.OrdinalIgnoreCase) ||
                  leave.LeaveType.Contains("paternity", StringComparison.OrdinalIgnoreCase)))
            {
                var start = leave.StartDate < firstDayOfMonth ? firstDayOfMonth : leave.StartDate;
                var end = leave.EndDate > lastDayOfMonth ? lastDayOfMonth : leave.EndDate;
                leaveDays += (end - start).Days + 1;
            }
        }

        // Account for Task Attendance
        var attendance = await _context.Attendances
            .Where(a => a.EmployeeId == employeeId && 
                        a.Date >= firstDayOfMonth && 
                        a.Date <= lastDayOfMonth)
            .ToListAsync();

        int absentDays = attendance.Count(a => a.Status.Equals("Absent", StringComparison.OrdinalIgnoreCase));

        decimal dailyRate = baseSalary / 22m;
        decimal leaveDeductions = Math.Round(leaveDays * dailyRate, 2);
        decimal absentDeductions = Math.Round(absentDays * dailyRate, 2);

        decimal basicPay = Math.Round(baseSalary * 0.70m, 2);
        decimal allowances = Math.Round(baseSalary * 0.30m, 2);

        return (basicPay, allowances, leaveDeductions, absentDeductions);
    }
}