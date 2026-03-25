namespace EmployeeManagement.Application.DTO.Request.Users;

public class UserRequest
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;

    public Guid RoleId { get; set; }
    public Guid EmployeeId { get; set; }
}