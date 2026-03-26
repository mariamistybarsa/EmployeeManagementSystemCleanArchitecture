using EmployeeManagement.Domain.Models;
using EmployeeManagement.Shared.DTO;

namespace EmployeeManagement.Persistence.Repositories.Interfaces.Users;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
}