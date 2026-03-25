using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Persistence.AppDbContext.Configuration;

public class SalaryDisbursementConfiguration : IEntityTypeConfiguration<SalaryDisbursement>
{
    public void Configure(EntityTypeBuilder<SalaryDisbursement> builder)
    {
        builder.ToTable("salary_disbursement");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("newsequentialid()");

        builder.Property(s => s.Month).HasColumnName("month");
        builder.Property(s => s.Year).HasColumnName("year");

        builder.Property(s => s.EmpId).HasColumnName("emp_id");

        builder.Property(s => s.BaseSalary).HasColumnName("base_salary").HasColumnType("decimal(18,2)");
        builder.Property(s => s.TotalAllowances).HasColumnName("total_allowances").HasColumnType("decimal(18,2)");
        builder.Property(s => s.TotalDeduction).HasColumnName("total_deduction").HasColumnType("decimal(18,2)");
        builder.Property(s => s.NetSalary).HasColumnName("net_salary").HasColumnType("decimal(18,2)");

        builder.Property(s => s.Status).HasColumnName("status").HasMaxLength(50);
        builder.Property(s => s.Remarks).HasColumnName("remarks").HasMaxLength(250);

        builder.Property(s => s.DisbursedBy).HasColumnName("disbursed_by");

        builder.Property(s => s.IsActive).HasColumnName("is_active").HasDefaultValue(true);
        builder.Property(s => s.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);

        builder.Property(s => s.CreatedDate).HasColumnName("created_date").HasColumnType("datetime2");

    }
}