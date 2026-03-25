using EmployeeManagement.Application.DTO.Request.Designations;
using EmployeeManagement.Application.DTO.Response.Designations;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Mappers.Designations;

public static class DesignationMapper
{
    // Create
    public static Designation MapRequestToEntity(DesignationRequest request) => new()
    {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Description = request.Description,
        IsActive = true,
        IsDeleted = false,
        CreatedBy = Guid.Empty,
        CreatedDate = DateTime.UtcNow
    };

    // Entity → Response
    public static DesignationResponse MapEntityToResponse(Designation entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Description = entity.Description,
        IsActive = entity.IsActive,
        IsDeleted = entity.IsDeleted,
        CreatedDate = entity.CreatedDate
    };

    // List mapping
    public static List<DesignationResponse> MapList(List<Designation> entities) =>
        entities.Select(MapEntityToResponse).ToList();
}