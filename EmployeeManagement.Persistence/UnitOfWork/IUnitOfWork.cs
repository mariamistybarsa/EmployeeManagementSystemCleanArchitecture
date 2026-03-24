using EmployeeManagement.Domain.Models;
using EmployeeManagement.Persistence.Repositories.Interfaces;
namespace EmployeeManagement.Persistence.UnitOfWork;
public interface IUnitOfWork
{
    IGenericRepository<Department> Departments { get; }
    IGenericRepository<Designation> Designations { get; }
    IGenericRepository<Employee> Employees { get; }

    Task<int> SaveChangesAsync();
}