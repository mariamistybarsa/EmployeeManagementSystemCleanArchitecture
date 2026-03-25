using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Persistence.AppDbContext;

public class EmployeeManagementDbContext(DbContextOptions<EmployeeManagementDbContext> options)
    : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Designation> Designations { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<SalaryAdjustment> SalaryAdjustments { get; set; }
    public DbSet<SalaryDisbursement> SalaryDisbursements { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeManagementDbContext).Assembly);
    }
}
