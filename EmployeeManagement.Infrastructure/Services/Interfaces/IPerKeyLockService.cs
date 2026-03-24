namespace EmployeeManagement.Infrastructure.Services.Interfaces;



public interface IPerKeyLockService
{
    Task<IDisposable> AcquireAsync(string key, CancellationToken cancellationToken = default);
}