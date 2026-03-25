using EmployeeManagement.Application.DTO.Request.Roles;
using EmployeeManagement.Application.Services.Interfaces.Generic;
using EmployeeManagement.Shared.DTO;

namespace EmployeeManagement.Application.Services.Interfaces.Roles;

public interface IRoleService : IGenericService<RoleRequest>
{
    Task<StandardResponse> UpdateRoleAsync(Guid id, RoleUpdateRequest request);
}