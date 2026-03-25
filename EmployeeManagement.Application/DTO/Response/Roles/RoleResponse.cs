namespace EmployeeManagement.Application.DTO.Response.Roles;

public class RoleResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }
}