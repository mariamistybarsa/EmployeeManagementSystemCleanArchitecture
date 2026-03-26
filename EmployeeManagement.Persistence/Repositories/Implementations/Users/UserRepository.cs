using EmployeeManagement.Domain.Models;
using EmployeeManagement.Persistence.AppDbContext;
using EmployeeManagement.Persistence.Repositories.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Persistence.Repositories.Implementations.Users;

public class UserRepository(EmployeeManagementDbContext dbContext) : GenericRepository<User>(dbContext), IUserRepository
{
    private readonly EmployeeManagementDbContext _dbContext = dbContext;

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return existingUser;
    }
}
