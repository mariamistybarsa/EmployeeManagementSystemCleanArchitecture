using EmployeeManagement.Domain.Models;
using EmployeeManagement.Persistence.Repositories.Interfaces;
using EmployeeManagement.Persistence.Repositories.Interfaces.Users;

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
 
    IUserRepository Users { get; }
    Task<int> SaveChangesAsync();
}