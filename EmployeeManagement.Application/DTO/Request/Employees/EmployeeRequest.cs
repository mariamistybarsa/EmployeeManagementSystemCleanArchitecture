using System;

namespace EmployeeManagement.Application.DTO.Request.Employees;

public class EmployeeRequest
{
    public Guid DepartmentId { get; set; }
    public Guid DesignationId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }

    public DateTime JoinDate { get; set; }
    public DateTime DOB { get; set; }

    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string PresentAddress { get; set; }
    public string EmpCode { get; set; }

    public decimal BaseSalary { get; set; }
}