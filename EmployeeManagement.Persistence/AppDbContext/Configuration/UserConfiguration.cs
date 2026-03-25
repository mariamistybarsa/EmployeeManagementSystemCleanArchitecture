using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Persistence.AppDbContext.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("newsequentialid()");

        builder.Property(u => u.UserName).HasColumnName("user_name").HasMaxLength(100).IsRequired();
        builder.Property(u => u.Password).HasColumnName("password").HasMaxLength(200).IsRequired();
        builder.Property(u => u.Email).HasColumnName("email").HasMaxLength(150);

        builder.Property(u => u.RoleId).HasColumnName("role_id");
        builder.Property(u => u.EmployeeId).HasColumnName("employee_id");

        builder.Property(u => u.IsActive).HasColumnName("is_active").HasDefaultValue(true);
        builder.Property(u => u.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);

        builder.Property(u => u.CreatedDate).HasColumnName("created_date").HasColumnType("datetime2");
        
    }
}