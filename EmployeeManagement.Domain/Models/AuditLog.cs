namespace EmployeeManagement.Domain.Models;

public class AuditLog
{
    public Guid Id { get; set; }
    public string Module { get; set; } = null!;
    public string Action { get; set; } = null!;
    public string ActionId { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string IpAddress { get; set; } = null!;
    public string? ExtraJson { get; set; }
    public DateTime CreatedDate { get; set; }
}