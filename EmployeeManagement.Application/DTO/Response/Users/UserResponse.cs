namespace EmployeeManagement.Application.DTO.Response.Users;

public class UserResponse
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public Guid RoleId { get; set; }
    public Guid EmployeeId { get; set; }

    public bool IsActive { get; set; }
}