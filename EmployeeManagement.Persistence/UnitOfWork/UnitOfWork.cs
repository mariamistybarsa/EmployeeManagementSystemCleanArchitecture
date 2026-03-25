using EmployeeManagement.Domain.Models;
using EmployeeManagement.Persistence.AppDbContext;
using EmployeeManagement.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly EmployeeManagementDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public UnitOfWork(EmployeeManagementDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }
    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public async Task BeginTransactionAsync()
    {
        if (_transaction != null) return;
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction == null)
            throw new InvalidOperationException("No transaction started.");

        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction == null)
            throw new InvalidOperationException("No transaction started.");

        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            _transaction?.Dispose();
            _context.Dispose();
        }

        _disposed = true;
    }
    
    private T GetRepository<T>() where T : class
        => _serviceProvider.GetRequiredService<T>();

    public IGenericRepository<Department> Departments => GetRepository<IGenericRepository<Department>>();
    public IGenericRepository<Designation> Designations => GetRepository<IGenericRepository<Designation>>();
    public IGenericRepository<Employee> Employees => GetRepository<IGenericRepository<Employee>>();
    public IGenericRepository<AuditLog> AuditLogs => GetRepository<IGenericRepository<AuditLog>>();
    public IGenericRepository<Role> Roles => GetRepository<IGenericRepository<Role>>();
    public IGenericRepository<User> Users => GetRepository<IGenericRepository<User>>();
    public IGenericRepository<SalaryDisbursement> SalaryDisbursements => GetRepository<IGenericRepository<SalaryDisbursement>>();
    public IGenericRepository<SalaryAdjustment> SalaryAdjustments => GetRepository<IGenericRepository<SalaryAdjustment>>();
   
}