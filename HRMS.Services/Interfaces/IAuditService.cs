using HRMS.Models.DTOs;

namespace HRMS.Services.Interfaces;

public interface IAuditService
{
    Task<IEnumerable<AuditLogResponse>> GetAllAsync();
    Task LogActionAsync(string entityName, string entityId, string action, string changes, string userId);
}