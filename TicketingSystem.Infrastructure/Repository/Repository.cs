using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.Core.IRepository;
using TicketingSystem.Infrastructure.Data;

namespace TicketingSystem.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            // SaveChanges removed - handled by UnitOfWork
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            // SaveChanges removed - handled by UnitOfWork
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            // SaveChanges removed - handled by UnitOfWork
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            // SaveChanges removed - handled by UnitOfWork
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            // SaveChanges removed - handled by UnitOfWork
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            // SaveChanges removed - handled by UnitOfWork
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                // SaveChanges removed - handled by UnitOfWork
            }
        }
    }
}