using Microsoft.EntityFrameworkCore;
using HRMS.Data;
using HRMS.Models.Entities;
using HRMS.Models.DTOs;
using HRMS.Services.Interfaces;

namespace HRMS.Services;

public class DesignationService : IDesignationService
{
    private readonly ApplicationDbContext _context;
    private readonly IAuditService _auditService;

    public DesignationService(ApplicationDbContext context, IAuditService auditService)
    {
        _context = context;
        _auditService = auditService;
    }

    public async Task<IEnumerable<DesignationResponse>> GetAllAsync()
    {
        return await _context.Designations
            .Select(x => new DesignationResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();
    }

    public async Task<DesignationResponse> CreateAsync(DesignationRequest request)
    {
        var designation = new Designation
        {
            Name = request.Name,
            Description = request.Description
        };

        _context.Designations.Add(designation);
        await _context.SaveChangesAsync();

        await _auditService.LogActionAsync("Designation", designation.Id.ToString(), "Create", $"Name: {designation.Name}", "System");

        return new DesignationResponse
        {
            Id = designation.Id,
            Name = designation.Name,
            Description = designation.Description
        };
    }

    public async Task<bool> UpdateAsync(DesignationRequest request)
    {
        var designation = await _context.Designations.FindAsync(request.Id);
        if (designation == null) return false;

        designation.Name = request.Name;
        designation.Description = request.Description;

        await _context.SaveChangesAsync();
        await _auditService.LogActionAsync("Designation", designation.Id.ToString(), "Update", $"Name: {designation.Name}", "System");
        return true;
    }

    public async Task DeleteAsync(int id)
    {
        var designation = await _context.Designations.FindAsync(id);
        if (designation != null)
        {
            _context.Designations.Remove(designation);
            await _context.SaveChangesAsync();
            await _auditService.LogActionAsync("Designation", id.ToString(), "Delete", "", "System");
        }
    }
}