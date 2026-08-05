using System.Collections.Generic;
using System.Threading.Tasks;
using HRMS.Models.DTOs;

namespace HRMS.Services.Interfaces;

public interface ILeaveService
{
    Task<IEnumerable<LeaveResponse>> GetAllAsync();
    Task<LeaveResponse> RequestAsync(LeaveRequest request);
    Task<bool> UpdateStatusAsync(int id, string status);
}