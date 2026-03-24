using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Persistence.DbContext;

public class EmployeeManagementDBContext : Microsoft.EntityFrameworkCore.DbContext
{
    public EmployeeManagementDBContext(DbContextOptions<EmployeeManagementDBContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Designation> Designations { get; set; }
}