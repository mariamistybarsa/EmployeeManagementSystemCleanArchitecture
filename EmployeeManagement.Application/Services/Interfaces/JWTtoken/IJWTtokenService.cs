using EmployeeManagement.Application.DTO.Response.Auth;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Services.Interfaces.JWTtoken;

public interface IJWTtokenService 
{
    Task<TokenCreationResponse>GenerateToken(User user , Employee employee);
}