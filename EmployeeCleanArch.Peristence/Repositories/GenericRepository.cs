using EmployeeCleanArch.Application.Interfaces.Repositories;
using EmployeeCleanArch.Domain.Common.Interfaces;
using EmployeeCleanArch.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCleanArch.Peristence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _dbcontext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _dbcontext = context;
            _dbSet = _dbcontext.Set<T>();
        }
        public IQueryable<T> Entities => _dbSet.AsQueryable();
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity); 
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbcontext.SaveChangesAsync();
            return entity;
        }
    }
}
