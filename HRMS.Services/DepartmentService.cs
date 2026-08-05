using HRMS.Data;
using HRMS.Models.Entities;
using HRMS.Models.DTOs;
using HRMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services;

public class DepartmentService : IDepartmentService
{
    private readonly ApplicationDbContext _context;

    public DepartmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DepartmentResponse>> GetAllAsync()
    {
        return await _context.Departments
            .Select(d => new DepartmentResponse
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description
            })
            .ToListAsync();
    }

    public async Task<DepartmentResponse?> GetByIdAsync(int id)
    {
        var d = await _context.Departments.FindAsync(id);
        if (d == null) return null;

        return new DepartmentResponse
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description
        };
    }

    public async Task<DepartmentResponse> CreateAsync(DepartmentRequest request)
    {
        var department = new Department
        {
            Name = request.Name,
            Description = request.Description
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return new DepartmentResponse
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };
    }

    public async Task<bool> UpdateAsync(DepartmentRequest request)
    {
        var department = await _context.Departments.FindAsync(request.Id);
        if (department == null) return false;

        department.Name = request.Name;
        department.Description = request.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) return false;

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return true;
    }
}