using EmployeeManagement.Application.DTO.Request.Users;
using EmployeeManagement.Application.DTO.Response.Users;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Mappers.Users;

public static class UserMapper
{
  
    public static User ToEntity(UserRequest request)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            Password = request.Password,
            Email = request.Email,
            RoleId = request.RoleId,
            EmployeeId = request.EmployeeId,
            IsActive = true,
            IsDeleted = false,
            CreatedDate = DateTime.UtcNow
        };
    }


    public static UserResponse ToResponse(User entity)
    {
        return new UserResponse
        {
            Id = entity.Id,
            UserName = entity.UserName,
            Email = entity.Email,
            RoleId = entity.RoleId,
            EmployeeId = entity.EmployeeId,
            IsActive = entity.IsActive
        };
    }
    
    public static User MapRequestToUpdatedEntity(User oldUser, UserUpdateRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.UserName))
            oldUser.UserName = request.UserName;

        if (!string.IsNullOrWhiteSpace(request.Email))
            oldUser.Email = request.Email;

        if (request.RoleId != Guid.Empty)
            oldUser.RoleId = request.RoleId ??  oldUser.RoleId;

        if (request.EmployeeId != Guid.Empty)
            oldUser.EmployeeId = request.EmployeeId ?? oldUser.EmployeeId;

        oldUser.UpdatedBy = Guid.Empty;

        return oldUser;
    }
}