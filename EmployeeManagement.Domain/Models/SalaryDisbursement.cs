namespace EmployeeManagement.Domain.Models;

public class SalaryDisbursement
{
    public Guid Id { get; set; }

    public int Month { get; set; }
    public int Year { get; set; }

    public Guid EmpId { get; set; }

    public decimal BaseSalary { get; set; }
    public decimal TotalAllowances { get; set; }
    public decimal TotalDeduction { get; set; }
    public decimal NetSalary { get; set; }

    public string Status { get; set; } = null!;
    public string? Remarks { get; set; }

    public Guid DisbursedBy { get; set; }

    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime CreatedDate { get; set; }

    // Navigation
    public Employee Employee { get; set; } = null!;
}