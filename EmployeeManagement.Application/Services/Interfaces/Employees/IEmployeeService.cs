using EmployeeManagement.Application.DTO.Request.Employees;
using EmployeeManagement.Application.Services.Interfaces.Generic;
using EmployeeManagement.Shared.DTO;

namespace EmployeeManagement.Application.Services.Interfaces.Employees;

public interface IEmployeeService : IGenericService<EmployeeRequest>

{
    Task<StandardResponse> UpdateEmployeeAsync(Guid id, EmployeeUpdateRequest request);
}