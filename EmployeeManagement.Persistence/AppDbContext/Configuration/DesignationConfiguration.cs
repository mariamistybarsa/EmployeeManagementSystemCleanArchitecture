using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Persistence.AppDbContext.Configuration;

public class DesignationConfiguration : IEntityTypeConfiguration<Designation>
{
    public void Configure(EntityTypeBuilder<Designation> builder)
    {
        builder.ToTable("designation");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("newsequentialid()");

        builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(d => d.Description).HasColumnName("description").HasMaxLength(250);

        builder.Property(d => d.IsActive).HasColumnName("is_active").HasDefaultValue(true);
        builder.Property(d => d.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);

        builder.Property(d => d.CreatedDate).HasColumnName("created_date").HasColumnType("datetime2");
    }
}