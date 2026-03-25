using EmployeeManagement.Domain.Models;
using EmployeeManagement.Persistence.Repositories.Interfaces;
namespace EmployeeManagement.Persistence.UnitOfWork;
public interface IUnitOfWork
{
    IGenericRepository<Department> Departments { get; }
    IGenericRepository<Designation> Designations { get; }
    IGenericRepository<Employee> Employees { get; }
    IGenericRepository<AuditLog> AuditLogs { get; }
    IGenericRepository<Role> Roles { get; }
    IGenericRepository<SalaryAdjustment> SalaryAdjustments { get; }
    IGenericRepository<SalaryDisbursement> SalaryDisbursements { get; }
    IGenericRepository<User> Users { get; }
    Task<int> SaveChangesAsync();
}