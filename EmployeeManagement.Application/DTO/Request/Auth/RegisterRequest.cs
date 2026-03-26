namespace EmployeeManagement.Application.DTO.Request.Auth;

public class RegisterRequest
{
    // 👤 Employee Info
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }

    public DateTime JoinDate { get; set; }
    public DateTime DOB { get; set; }

    public string PhoneNumber { get; set; }
    public string PresentAddress { get; set; }

    public Guid DepartmentId { get; set; }
    public Guid DesignationId { get; set; }

    public decimal BaseSalary { get; set; }

    // 🔐 Auth Info (User)
    public string Email { get; set; }
    public string Password { get; set; }

    public Guid RoleId { get; set; }

    // Optional
    public string? UserName { get; set; }
}