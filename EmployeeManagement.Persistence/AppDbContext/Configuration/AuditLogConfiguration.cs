using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Persistence.AppDbContext.Configuration;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> entity)
    {
        entity.ToTable("audit_log");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasDefaultValueSql("newsequentialid()");

        entity.Property(e => e.Module)
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(e => e.Action)
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(e => e.ActionId)
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(e => e.Message)
            .HasMaxLength(500)
            .IsRequired();

        entity.Property(e => e.IpAddress)
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(e => e.CreatedDate)
            .HasColumnType("datetime2");
    }
}