using HRMS.Data;
using HRMS.Models.Entities;
using HRMS.Models.DTOs;
using HRMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EmployeeResponse>> GetAllAsync()
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Select(e => new EmployeeResponse
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department.Name,
                BaseSalary = e.BaseSalary
            })
            .ToListAsync();
    }

    public async Task<EmployeeResponse?> GetByIdAsync(int id)
    {
        var e = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(x => x.Id == id);
            
        if (e == null) return null;

        return new EmployeeResponse
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            DepartmentId = e.DepartmentId,
            DepartmentName = e.Department.Name,
            BaseSalary = e.BaseSalary
        };
    }

    public async Task<EmployeeResponse?> GetByEmailAsync(string email)
    {
        var e = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(x => x.Email == email);
            
        if (e == null) return null;

        return new EmployeeResponse
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            DepartmentId = e.DepartmentId,
            DepartmentName = e.Department.Name,
            BaseSalary = e.BaseSalary
        };
    }

    public async Task<EmployeeResponse> CreateAsync(EmployeeRequest request)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            DepartmentId = request.DepartmentId,
            BaseSalary = request.BaseSalary,
            UserId = request.UserId
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        // Reload to get department name
        return (await GetByIdAsync(employee.Id))!;
    }

    public async Task<bool> UpdateAsync(EmployeeRequest request)
    {
        var employee = await _context.Employees.FindAsync(request.Id);
        if (employee == null) return false;

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Email = request.Email;
        employee.DepartmentId = request.DepartmentId;
        employee.BaseSalary = request.BaseSalary;
        employee.UserId = request.UserId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return false;

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }
}