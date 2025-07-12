using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;
using TicketingSystem.Core.IRepository;
using TicketingSystem.Infrastructure.Data;

namespace TicketingSystem.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories;
        private IDbContextTransaction? _transaction;
        private ITicketRepository? _ticketRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _repositories = new ConcurrentDictionary<Type, object>();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            return (IRepository<T>)_repositories.GetOrAdd(type, _ => new Repository<T>(_context));
        }

        public ITicketRepository TicketRepository =>
            _ticketRepository ??= new TicketRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}
