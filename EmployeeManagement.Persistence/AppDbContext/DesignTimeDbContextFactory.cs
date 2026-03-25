using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EmployeeManagement.Persistence.AppDbContext;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EmployeeManagementDbContext>
{
    public EmployeeManagementDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EmployeeManagementDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=EMS_DB;TrustServerCertificate=True"
        );

        return new EmployeeManagementDbContext(optionsBuilder.Options);
    }
}