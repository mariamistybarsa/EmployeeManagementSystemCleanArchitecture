namespace EmployeeManagement.Application.DTO.Request.SalaryAdjustment;

public class SalaryAdjustmentUpdateRequest
{
   
    public Guid? EmpId { get; set; }
    public string? AdjustmentType { get; set; }
    public string? AdjustmentName { get; set; }
    public decimal? Amount { get; set; }
    public string? Reason { get; set; }
    public int? EffectiveMonth { get; set; }
    public int? EffectiveYear { get; set; }
}
    
