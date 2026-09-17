using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRMS.Data;
using HRMS.Models.Entities;
using HRMS.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.UI.Controllers;

[ApiController]
[Route("api/employees")]
[Authorize]
public class EmployeesApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EmployeesApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeApiDto>>> GetEmployees()
    {
        var employees = await _context.Employees
            .Include(e => e.Department)
            .Select(e => new EmployeeApiDto
            {
                Id = e.Id,
                Name = e.FirstName + " " + e.LastName,
                Email = e.Email,
                Department = e.Department != null ? e.Department.Name : string.Empty,
                BaseSalary = e.BaseSalary
            })
            .ToListAsync();

        return Ok(employees);
    }

    [HttpGet("{id}/balance")]
    public async Task<ActionResult<EmployeeBalanceDto>> GetEmployeeBalance(int id)
    {
        var employeeExists = await _context.Employees.AnyAsync(e => e.Id == id);
        if (!employeeExists)
        {
            return NotFound(new { message = "Employee not found." });
        }

        var balanceRecord = await _context.EmployeeSalaryBalances
            .FirstOrDefaultAsync(b => b.EmployeeId == id);

        decimal amountBalance = balanceRecord?.AmountBalance ?? 0;

        var pendingWithdrawals = await _context.WithdrawalApplications
            .Where(w => w.SelectedEmployeeId == id && (w.StatusId == 1 || w.StatusId == 2 || w.StatusId == 3))
            .SumAsync(w => w.Amount);

        var netBalance = amountBalance - pendingWithdrawals;

        var result = new EmployeeBalanceDto
        {
            EmployeeId = id,
            AmountBalance = amountBalance,
            PendingWithdrawals = pendingWithdrawals,
            NetBalance = netBalance
        };

        return Ok(result);
    }
}
