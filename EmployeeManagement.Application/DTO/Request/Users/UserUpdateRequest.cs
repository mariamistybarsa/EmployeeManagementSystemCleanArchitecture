namespace EmployeeManagement.Application.DTO.Request.Users;

public class UserUpdateRequest
{
    public string? UserName { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string? Password { get; set; }    
    public Guid? RoleId { get; set; }
    public Guid? EmployeeId { get; set; }
}