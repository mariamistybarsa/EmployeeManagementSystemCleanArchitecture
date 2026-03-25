using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Persistence.AppDbContext.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("role");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("newsequentialid()");

        builder.Property(r => r.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(r => r.Description).HasColumnName("description").HasMaxLength(250);

        builder.Property(r => r.IsActive).HasColumnName("is_active").HasDefaultValue(true);
        builder.Property(r => r.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);

        builder.Property(r => r.CreatedDate).HasColumnName("created_date").HasColumnType("datetime2");
    }
}