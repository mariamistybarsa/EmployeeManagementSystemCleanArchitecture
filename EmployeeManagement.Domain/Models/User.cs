namespace EmployeeManagement.Domain.Models;

public class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;

    public Guid RoleId { get; set; }
    public Guid EmployeeId { get; set; }

    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime CreatedDate { get; set; }

    // Navigation
    public Role Role { get; set; } = null!;
    public Employee Employee { get; set; } = null!;
}