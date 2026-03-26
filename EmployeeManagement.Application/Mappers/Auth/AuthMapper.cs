using EmployeeManagement.Application.DTO.Request.Auth;
using EmployeeManagement.Application.DTO.Response.Auth;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Mappers.Auth;

public static class AuthMapper
{
    public static Employee ToEmployee(RegisterRequest request)
    {
        return new Employee
        {
            Id = Guid.NewGuid(),

            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender,

            JoinDate = request.JoinDate,
            DOB = request.DOB,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            PresentAddress = request.PresentAddress,

            DepartmentId = request.DepartmentId,
            DesignationId = request.DesignationId,

            BaseSalary = request.BaseSalary,

            EMP_Code = $"EMP-{DateTime.UtcNow.Ticks}",

            IsActive = true,
            IsDeleted = false,

            CreatedDate = DateTime.UtcNow
        };
    }

    public static User ToUser(RegisterRequest request, Guid employeeId)
    {
        return new User
        {
            Id = Guid.NewGuid(),

            UserName = string.IsNullOrWhiteSpace(request.UserName)
                ? request.Email
                : request.UserName,

            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),

            RoleId = request.RoleId,
            EmployeeId = employeeId,

            IsActive = true,
            IsDeleted = false,

            CreatedDate = DateTime.UtcNow
        };
    }

    public static RegisterResponse MapToResponse(RegisterRequest request)
    {
        return new RegisterResponse
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender,

            JoinDate = request.JoinDate,
            DOB = request.DOB,

            PhoneNumber = request.PhoneNumber,
            PresentAddress = request.PresentAddress,

            DepartmentId = request.DepartmentId,
            DesignationId = request.DesignationId,

            BaseSalary = request.BaseSalary,

            Email = request.Email,
            RoleId = request.RoleId,

            UserName = string.IsNullOrWhiteSpace(request.UserName)
                ? request.Email
                : request.UserName
        };
    }
}