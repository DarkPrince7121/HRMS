using HRMS.Models.DTOs;

namespace HRMS.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeResponse>> GetAllAsync();
    Task<EmployeeResponse?> GetByIdAsync(int id);
    Task<EmployeeResponse?> GetByEmailAsync(string email);
    Task<EmployeeResponse> CreateAsync(EmployeeRequest request);
    Task<bool> UpdateAsync(EmployeeRequest request);
    Task<bool> DeleteAsync(int id);
}