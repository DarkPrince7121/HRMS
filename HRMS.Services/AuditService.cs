using Microsoft.EntityFrameworkCore;
using HRMS.Data;
using HRMS.Models.Entities;
using HRMS.Models.DTOs;
using HRMS.Services.Interfaces;

namespace HRMS.Services;

public class AuditService : IAuditService
{
    private readonly ApplicationDbContext _context;

    public AuditService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuditLogResponse>> GetAllAsync()
    {
        return await _context.AuditLogs
            .OrderByDescending(x => x.Timestamp)
            .Select(x => new AuditLogResponse
            {
                Id = x.Id,
                EntityName = x.EntityName,
                EntityId = x.EntityId,
                Action = x.Action,
                Changes = x.Changes,
                UserId = x.UserId,
                Timestamp = x.Timestamp
            }).ToListAsync();
    }

    public async Task LogActionAsync(string entityName, string entityId, string action, string changes, string userId)
    {
        var log = new AuditLog
        {
            EntityName = entityName,
            EntityId = entityId,
            Action = action,
            Changes = changes,
            UserId = userId,
            Timestamp = DateTime.UtcNow
        };

        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync();
    }
}