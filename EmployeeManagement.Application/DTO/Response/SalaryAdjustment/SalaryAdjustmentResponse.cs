namespace EmployeeManagement.Application.DTO.Response.SalaryAdjustment;

public class SalaryAdjustmentResponse
{
    public Guid EmpId { get; set; }
    public Guid Id { get; set; }
    public string AdjustmentType { get; set; } = null!;
    public string AdjustmentName { get; set; } = null!;
    public decimal Amount { get; set; }

    public string Reason { get; set; } = null!;

    public int EffectiveMonth { get; set; }
 
    public int EffectiveYear { get; set; }
}