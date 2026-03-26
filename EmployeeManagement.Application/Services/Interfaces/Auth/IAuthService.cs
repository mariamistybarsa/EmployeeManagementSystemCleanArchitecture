using EmployeeManagement.Application.DTO.Request.Auth;
using EmployeeManagement.Application.Services.Interfaces.Generic;
using EmployeeManagement.Shared.DTO;

namespace EmployeeManagement.Application.Services.Interfaces.Auth;

public interface IAuthService 
{
    Task<StandardResponse> LoginAsync(LoginRequest request);
    Task<StandardResponse> RegisterAsync(RegisterRequest request);

}