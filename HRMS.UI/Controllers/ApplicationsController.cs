using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRMS.Data;
using HRMS.Models.Entities;
using HRMS.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace HRMS.UI.Controllers;

[Authorize]
public class ApplicationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ApplicationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // MVC View
    public IActionResult Index()
    {
        return View();
    }

    // API: Get all applications
    [HttpGet]
    [Route("api/applications")]
    public async Task<IActionResult> GetApplications()
    {
        var query = _context.WithdrawalApplications
            .Include(w => w.Status)
            .AsQueryable();

        var applications = await query.ToListAsync();
        
        var employeeIds = applications.Where(w => w.SelectedEmployeeId.HasValue).Select(w => w.SelectedEmployeeId!.Value).Distinct().ToList();
        var employees = await _context.Employees
            .Where(e => employeeIds.Contains(e.Id))
            .ToDictionaryAsync(e => e.Id, e => new { Name = e.FirstName + " " + e.LastName, Email = e.Email });

        var dtos = applications.Select(w => new WithdrawalApplicationDto
        {
            Id = w.Id,
            StatusId = w.StatusId,
            StatusName = w.Status != null ? w.Status.Name : "Unknown",
            CreatedPersonId = w.CreatedPersonId,
            CreatedDate = w.CreatedDate,
            LastChangeDate = w.LastChangeDate,
            StatusLastChangeDate = w.StatusLastChangeDate,
            Amount = w.Amount,
            SelectedEmployeeId = w.SelectedEmployeeId,
            SelectedEmployeeName = w.SelectedEmployeeId.HasValue && employees.ContainsKey(w.SelectedEmployeeId.Value) ? employees[w.SelectedEmployeeId.Value].Name : "N/A",
            SelectedEmployeeEmail = w.SelectedEmployeeId.HasValue && employees.ContainsKey(w.SelectedEmployeeId.Value) ? employees[w.SelectedEmployeeId.Value].Email : string.Empty
        }).ToList();

        return Ok(dtos);
    }

    // API: Initiate a new or resume existing in-progress withdrawal application
    [HttpPost]
    [Route("api/applications/initiate")]
    public async Task<IActionResult> InitiateApplication()
    {
        // Try to find an existing in-progress application (StatusId != 4)
        var existingApp = await _context.WithdrawalApplications
            .Include(w => w.Status)
            .FirstOrDefaultAsync(w => w.StatusId != 4);

        if (existingApp != null)
        {
            var employeeName = "N/A";
            var employeeEmail = "";

            if (existingApp.SelectedEmployeeId.HasValue)
            {
                var emp = await _context.Employees.FindAsync(existingApp.SelectedEmployeeId.Value);
                if (emp != null)
                {
                    employeeName = emp.FirstName + " " + emp.LastName;
                    employeeEmail = emp.Email;
                }
            }

            return Ok(new WithdrawalApplicationDto
            {
                Id = existingApp.Id,
                StatusId = existingApp.StatusId,
                StatusName = existingApp.Status != null ? existingApp.Status.Name : "Unknown",
                CreatedPersonId = existingApp.CreatedPersonId,
                CreatedDate = existingApp.CreatedDate,
                LastChangeDate = existingApp.LastChangeDate,
                StatusLastChangeDate = existingApp.StatusLastChangeDate,
                Amount = existingApp.Amount,
                SelectedEmployeeId = existingApp.SelectedEmployeeId,
                SelectedEmployeeName = employeeName,
                SelectedEmployeeEmail = employeeEmail
            });
        }

        // Otherwise create a new application
        var newApp = new WithdrawalApplication
        {
            StatusId = 1, // Application Created
            CreatedPersonId = 1, // Default or system user ID
            CreatedDate = DateTime.UtcNow,
            LastChangeDate = DateTime.UtcNow,
            StatusLastChangeDate = DateTime.UtcNow,
            Amount = 0,
            SelectedEmployeeId = null
        };

        _context.WithdrawalApplications.Add(newApp);
        await _context.SaveChangesAsync();

        // Refresh status navigation property
        await _context.Entry(newApp).Reference(w => w.Status).LoadAsync();

        return Ok(new WithdrawalApplicationDto
        {
            Id = newApp.Id,
            StatusId = newApp.StatusId,
            StatusName = newApp.Status != null ? newApp.Status.Name : "Application Created",
            CreatedPersonId = newApp.CreatedPersonId,
            CreatedDate = newApp.CreatedDate,
            LastChangeDate = newApp.LastChangeDate,
            StatusLastChangeDate = newApp.StatusLastChangeDate,
            Amount = newApp.Amount,
            SelectedEmployeeId = newApp.SelectedEmployeeId,
            SelectedEmployeeName = "N/A"
        });
    }
    [HttpGet]
    [Route("api/applications/initiate")]
    public async Task<IActionResult> GetInitiated() => await InitiateApplication();

    // API: Update application status
    [HttpPut]
    [Route("api/applications/{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto dto)
    {
        var app = await _context.WithdrawalApplications.FirstOrDefaultAsync(w => w.Id == id);
        if (app == null) return NotFound(new { message = "Application not found." });

        bool statusChanged = app.StatusId != dto.StatusId;
        app.StatusId = dto.StatusId;
        
        if (dto.Amount.HasValue) 
            app.Amount = dto.Amount.Value;
            
        if (dto.SelectedEmployeeId.HasValue) 
            app.SelectedEmployeeId = dto.SelectedEmployeeId.Value;

        app.LastChangeDate = DateTime.UtcNow;
        if (statusChanged)
        {
            app.StatusLastChangeDate = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return Ok(new { success = true });
    }

    // API: Complete/Submit withdrawal application and deduct salary balance
    [HttpPost]
    [Route("api/applications/{id}/complete")]
    public async Task<IActionResult> CompleteApplication(int id)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var app = await _context.WithdrawalApplications.FirstOrDefaultAsync(w => w.Id == id);
            if (app == null)
            {
                return NotFound(new { message = "Application not found." });
            }

            if (app.StatusId == 4)
            {
                return BadRequest(new { message = "Application is already submitted/completed." });
            }

            if (!app.SelectedEmployeeId.HasValue)
            {
                return BadRequest(new { message = "No target employee selected." });
            }

            var balance = await _context.EmployeeSalaryBalances
                .FirstOrDefaultAsync(b => b.EmployeeId == app.SelectedEmployeeId.Value);

            if (balance == null)
            {
                return BadRequest(new { message = "Employee salary balance record not found." });
            }

            if (balance.AmountBalance < app.Amount)
            {
                return BadRequest(new { message = "Insufficient salary balance for withdrawal." });
            }

            // Perform reduction
            balance.AmountBalance -= app.Amount;

            // Mark application submitted
            app.StatusId = 4; // Application Submitted
            app.LastChangeDate = DateTime.UtcNow;
            app.StatusLastChangeDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new { success = true, applicationId = app.Id });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, new { message = "Internal error during application completion.", error = ex.Message });
        }
    }
}
