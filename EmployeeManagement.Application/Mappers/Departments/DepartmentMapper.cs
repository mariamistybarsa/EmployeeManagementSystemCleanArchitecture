using EmployeeManagement.Application.DTO.Request.Department;
using EmployeeManagement.Application.DTO.Response.Department;
using EmployeeManagement.Domain.Models;
namespace EmployeeManagement.Application.Mappers.Departments;

public static class DepartmentMapper
{
    // 🔹 Create
    public static Department MapRequestToEntity(DepartmentRequest request) => new()
    {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Description = request.Description,
        IsActive = true,
        IsDeleted = false,
        CreatedBy = Guid.Empty,
        CreatedDate = DateTime.UtcNow
    };
    
    public static DepartmentResponse MapEntityToResponse(Department model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        Description = model.Description
    };

    // 🔹 Update
    public static Department MapRequestToUpdatedEntity(Department oldDepartment, DepartmentRequest request)
    {
        oldDepartment.Name = request.Name;
        oldDepartment.Description = request.Description;
        oldDepartment.UpdatedBy = Guid.Empty;

        return oldDepartment;
    }

    // 🔹 List Mapping
    public static List<DepartmentResponse> MapEntityListToResponseList(IEnumerable<Department> models) =>
        models.Select(MapEntityToResponse).ToList();

    // 🔹 Clone (for logging/audit later)
    public static Department CloneEntity(Department oldDepartmentDetails) => new()
    {
        Id = oldDepartmentDetails.Id,
        Name = oldDepartmentDetails.Name,
        Description = oldDepartmentDetails.Description,
        IsActive = oldDepartmentDetails.IsActive,
        IsDeleted = oldDepartmentDetails.IsDeleted,
        CreatedBy = oldDepartmentDetails.CreatedBy,
        CreatedDate = oldDepartmentDetails.CreatedDate,
        UpdatedBy = oldDepartmentDetails.UpdatedBy
    };
}