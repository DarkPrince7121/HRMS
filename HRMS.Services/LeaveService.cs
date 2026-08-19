using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Data;
using HRMS.Models.DTOs;
using HRMS.Models.Entities;
using HRMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services;

public class LeaveService : ILeaveService
{
    private readonly ApplicationDbContext _context;

    public LeaveService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LeaveResponse>> GetAllAsync()
    {        return await _context.Leaves
            .Include(l => l.Employee)
            .Select(l => new LeaveResponse
            {
                Id = l.Id,
                EmployeeId = l.EmployeeId,
                EmployeeName = l.Employee.FirstName + " " + l.Employee.LastName,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                LeaveType = l.LeaveType,
                Reason = l.Reason,
                Status = l.Status
            })
            .ToListAsync();
    }

    public async Task<LeaveResponse> RequestAsync(LeaveRequest request)
    {        // Ensure UTC Kind for PostgreSQL timestamp with time zone
        var startDateUtc = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
        var endDateUtc = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);

        var leave = new Leave
        {
            EmployeeId = request.EmployeeId,
            StartDate = startDateUtc,
            EndDate = endDateUtc,
            LeaveType = request.LeaveType,
            Reason = request.Reason,
            Status = "Pending"
        };

        _context.Leaves.Add(leave);
        await _context.SaveChangesAsync();

        var employee = await _context.Employees.FindAsync(request.EmployeeId);

        return new LeaveResponse
        {
            Id = leave.Id,
            EmployeeId = leave.EmployeeId,
            EmployeeName = employee != null ? employee.FirstName + " " + employee.LastName : "Unknown",
            StartDate = leave.StartDate,
            EndDate = leave.EndDate,
            LeaveType = leave.LeaveType,
            Reason = leave.Reason,
            Status = leave.Status
        };
    }

    public async Task<bool> UpdateStatusAsync(int id, string status)
    {        var leave = await _context.Leaves.FindAsync(id);
        if (leave == null) return false;

        leave.Status = status;
        await _context.SaveChangesAsync();
        return true;
    }
}