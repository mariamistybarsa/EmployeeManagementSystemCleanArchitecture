namespace EmployeeManagement.Domain.Models;

public class Employee
{
    public Guid Id { get; set; }

    public Guid DepartmentId { get; set; }
    public Guid DesignationId { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Gender { get; set; } = null!;

    public DateTime JoinDate { get; set; }
    public DateTime DOB { get; set; }

    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string PresentAddress { get; set; } = null!;
    public string EMP_Code { get; set; } = null!;

    public decimal BaseSalary { get; set; }

    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime CreatedDate { get; set; }

    // Navigation
    public Department Department { get; set; } = null!;
    public Designation Designation { get; set; } = null!;
}