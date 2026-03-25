namespace EmployeeManagement.Persistence.AppDbContext.Configuration;

using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employee");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("newsequentialid()");

        builder.Property(e => e.FirstName).HasColumnName("first_name").HasMaxLength(100).IsRequired();
        builder.Property(e => e.LastName).HasColumnName("last_name").HasMaxLength(100).IsRequired();
        builder.Property(e => e.Gender).HasColumnName("gender").HasMaxLength(20);

        builder.Property(e => e.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20);
        builder.Property(e => e.Email).HasColumnName("email").HasMaxLength(150);

        builder.Property(e => e.Password).HasColumnName("password").HasMaxLength(200);

        builder.Property(e => e.PresentAddress).HasColumnName("present_address").HasMaxLength(250);

        builder.Property(e => e.EMP_Code).HasColumnName("emp_code").HasMaxLength(50);

        builder.Property(e => e.BaseSalary).HasColumnName("base_salary").HasColumnType("decimal(18,2)");

        builder.Property(e => e.JoinDate).HasColumnName("join_date").HasColumnType("datetime2");
        builder.Property(e => e.DOB).HasColumnName("dob").HasColumnType("datetime2");

        builder.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
        builder.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);

        builder.Property(e => e.CreatedDate).HasColumnName("created_date").HasColumnType("datetime2");

        builder.Property(e => e.CreatedBy).HasColumnName("created_by");
        builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");

        builder.Property(e => e.DepartmentId).HasColumnName("department_id");
        builder.Property(e => e.DesignationId).HasColumnName("designation_id");

    }
}