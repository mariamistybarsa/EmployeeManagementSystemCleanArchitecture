using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Mappers.Auth;

public static class LoginMapper
{
    public static object ToLoginResponse(User user, string token)
    {
        return new
        {
            Token = token,
            user.Id,
            user.Email,
            user.UserName,
            user.RoleId
        };
    }
}