using EmployeeManagement.Application.DTO.Request.Roles;
using EmployeeManagement.Application.DTO.Response.Roles;

namespace EmployeeManagement.Application.Mappers.Roles;
using EmployeeManagement.Application.DTO.Request;
using EmployeeManagement.Shared.DTO.Response;
using EmployeeManagement.Domain.Models;

public static class RoleMapper
{
    // 🔹 CREATE
    public static Role ToEntity(RoleRequest request)
    {
        return new Role
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            IsActive = true,
            IsDeleted = false,
            CreatedDate = DateTime.UtcNow
        };
    }

    // 🔹 RESPONSE
    public static RoleResponse ToResponse(Role entity)
    {
        return new RoleResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            IsActive = entity.IsActive
        };
    }

    // 🔹 UPDATE (partial like Employee)
    public static Role MapRequestToUpdatedEntity(Role oldRole, RoleUpdateRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.Name))
            oldRole.Name = request.Name;

        if (!string.IsNullOrWhiteSpace(request.Description))
            oldRole.Description = request.Description;

        oldRole.UpdatedBy = Guid.Empty;

        return oldRole;
    }
}