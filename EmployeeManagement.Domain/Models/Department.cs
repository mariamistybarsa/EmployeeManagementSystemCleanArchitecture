namespace EmployeeManagement.Domain.Models;

public class Department
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime CreatedDate { get; set; }

}