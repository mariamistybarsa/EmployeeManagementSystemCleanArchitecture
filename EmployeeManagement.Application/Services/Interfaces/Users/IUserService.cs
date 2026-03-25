using EmployeeManagement.Application.DTO.Request.Users;
using EmployeeManagement.Application.Services.Interfaces.Generic;
using EmployeeManagement.Shared.DTO;

namespace EmployeeManagement.Application.Services.Interfaces.Users;

public interface IUserService : IGenericService<UserRequest>
{
    Task<StandardResponse> UpdateUserAsync(Guid id, UserUpdateRequest request);
}