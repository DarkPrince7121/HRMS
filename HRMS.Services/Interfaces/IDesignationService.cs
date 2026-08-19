using HRMS.Models.DTOs;

namespace HRMS.Services.Interfaces;

public interface IDesignationService
{
    Task<IEnumerable<DesignationResponse>> GetAllAsync();
    Task<DesignationResponse> CreateAsync(DesignationRequest request);
    Task<bool> UpdateAsync(DesignationRequest request);
    Task DeleteAsync(int id);
}