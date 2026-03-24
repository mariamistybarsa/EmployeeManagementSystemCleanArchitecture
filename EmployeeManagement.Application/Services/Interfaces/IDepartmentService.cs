using EmployeeManagement.Application.DTO.Request;
using EmployeeManagement.Shared.DTO;

namespace EmployeeManagement.Application.Services.Interfaces;

public interface IDepartmentService
{
    Task<StandardResponse> GetAllAsync();
    Task<StandardResponse> CreateAsync(DepartmentRequest request);
    Task<StandardResponse> DeleteAsync(Guid id);
    Task<StandardResponse> UpdateAsync(Guid id, DepartmentRequest request);
}