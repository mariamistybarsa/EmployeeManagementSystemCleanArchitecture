namespace EmployeeManagement.Domain.Models;

public class SalaryAdjustment
{
    public Guid Id { get; set; }

    public Guid EmpId { get; set; }
    public Guid DisburseId { get; set; }

    public string AdjustmentType { get; set; } = null!;
    public string AdjustmentName { get; set; } = null!;
    public decimal Amount { get; set; }

    public string Reason { get; set; } = null!;

    public int EffectiveMonth { get; set; }
    public int EffectiveYear { get; set; }

    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime CreatedDate { get; set; }

    // Navigation
    public Employee Employee { get; set; } = null!;
    public SalaryDisbursement SalaryDisbursement { get; set; } = null!;
}