namespace EmployeeManagement.Application.DTO.Response.Auth;

public class RegisterResponse
{
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
    
    public string Email { get; set; }
    
    public Guid RoleId { get; set; }

    public string? UserName { get; set; }
}