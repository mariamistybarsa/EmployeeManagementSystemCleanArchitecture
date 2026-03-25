using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Persistence.AppDbContext.Configuration;

public class SalaryAdjustmentConfiguration : IEntityTypeConfiguration<SalaryAdjustment>
{
    public void Configure(EntityTypeBuilder<SalaryAdjustment> builder)
    {
        builder.ToTable("salary_adjustment");

        // Primary Key
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("newsequentialid()");

        // Foreign Keys
        builder.Property(s => s.EmpId).HasColumnName("emp_id").IsRequired();
        builder.Property(s => s.DisburseId).HasColumnName("disburse_id").IsRequired();

        // Properties
        builder.Property(s => s.AdjustmentType)
            .HasColumnName("adjustment_type")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(s => s.AdjustmentName)
            .HasColumnName("adjustment_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.Amount)
            .HasColumnName("amount")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(s => s.Reason)
            .HasColumnName("reason")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(s => s.EffectiveMonth)
            .HasColumnName("effective_month")
            .IsRequired();

        builder.Property(s => s.EffectiveYear)
            .HasColumnName("effective_year")
            .IsRequired();

        builder.Property(s => s.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);

        builder.Property(s => s.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false);

        builder.Property(s => s.CreatedDate)
            .HasColumnName("created_date")
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(s => s.CreatedBy)
            .HasColumnName("created_by");

        builder.Property(s => s.UpdatedBy)
            .HasColumnName("updated_by");

     
        // ❌ IGNORE navigation (IMPORTANT)
        builder.Ignore(s => s.Employee);
        builder.Ignore(s => s.SalaryDisbursement);
    }
}