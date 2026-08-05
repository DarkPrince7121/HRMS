using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Data;
using HRMS.Models.DTOs;
using HRMS.Models.Entities;
using HRMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services;

public class AttendanceService : IAttendanceService
{
    private readonly ApplicationDbContext _context;

    public AttendanceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AttendanceResponse>> GetAllAsync()
    {
        return await _context.Attendances
            .Include(a => a.Employee)
            .Select(a => new AttendanceResponse
            {
                Id = a.Id,
                EmployeeId = a.EmployeeId,
                EmployeeName = a.Employee.FirstName + " " + a.Employee.LastName,
                Date = a.Date,
                Status = a.Status,
                Remarks = a.Remarks
            })
            .ToListAsync();
    }

    public async Task<AttendanceResponse> MarkAsync(AttendanceRequest request)
    {
        var attendance = new Attendance
        {
            EmployeeId = request.EmployeeId,
            Date = request.Date,
            Status = request.Status,
            Remarks = request.Remarks
        };

        _context.Attendances.Add(attendance);
        await _context.SaveChangesAsync();

        var employee = await _context.Employees.FindAsync(request.EmployeeId);
        
        return new AttendanceResponse
        {
            Id = attendance.Id,
            EmployeeId = attendance.EmployeeId,
            EmployeeName = employee != null ? employee.FirstName + " " + employee.LastName : "Unknown",
            Date = attendance.Date,
            Status = attendance.Status,
            Remarks = attendance.Remarks
        };
    }
}