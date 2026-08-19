using HRMS.Models.DTOs;

namespace HRMS.Services.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentResponse>> GetAllAsync();
    Task<DepartmentResponse?> GetByIdAsync(int id);
    Task<DepartmentResponse> CreateAsync(DepartmentRequest request);
    Task<bool> UpdateAsync(DepartmentRequest request);
    Task<bool> DeleteAsync(int id);
}