// using Microsoft.EntityFrameworkCore;
// using EmployeeManagement.Persistence.DbContext;
// namespace EmployeeManagement.Persistence.Repositories.Implementations;
// using EmployeeManagement.Persistence.Repositories.Interfaces;
//
// public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
// {
//     private readonly EmployeeManagementDBContext _context;
//     private readonly DbSet<TEntity> _dbSet;
//
//     public GenericRepository(EmployeeManagementDBContext context)
//     {
//         _context = context;
//         _dbSet = _context.Set<TEntity>();
//     }
//
//     public async Task<List<TEntity>> GetAllAsync()
//     {
//         return await _dbSet.ToListAsync();
//     }
//
//     public async Task<TEntity?> GetByIdAsync(Guid id)
//     {
//         return await _dbSet.FindAsync(id);
//     }
//
//     public async Task AddAsync(TEntity entity)
//     {
//         await _dbSet.AddAsync(entity);
//     }
//
//     public void Update(TEntity entity)
//     {
//         _dbSet.Update(entity);
//     }
//
//     public void Delete(TEntity entity)
//     {
//         _dbSet.Remove(entity);
//     }
// }