namespace EmployeeManagement.Application.DTO.Request.SalaryAdjustment;

public class SalaryAdjustmentRequest
{
    public Guid EmpId { get; set; }

    public string AdjustmentType { get; set; } = null!; // Allowance / Deduction
    public string AdjustmentName { get; set; } = null!;
    public decimal Amount { get; set; }

    public string Reason { get; set; } = null!;

    public int EffectiveMonth { get; set; }
    public int EffectiveYear { get; set; }
    
}