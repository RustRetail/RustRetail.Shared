using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using RustRetail.SharedKernel.Domain.Abstractions;
using RustRetail.SharedKernel.Domain.Models;
using System.Collections.Concurrent;

namespace RustRetail.SharedPersistence.Database
{
    public abstract class UnitOfWork<TDbContext>
        : IUnitOfWork
        where TDbContext : DbContext
    {
        protected readonly TDbContext _context;
        protected readonly IServiceProvider _serviceProvider;

        protected IDbContextTransaction? _currentTransaction;
        protected readonly ConcurrentDictionary<Type, object> _repositoryCache = new();

        public UnitOfWork(TDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public bool HasActiveTransaction => _currentTransaction != null;

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already active. Please commit or rollback the current transaction before starting a new one.");
            }
            _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("No active transaction to commit.");
            }

            await _context.Database.CommitTransactionAsync(cancellationToken);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public void Dispose()
        {
            _currentTransaction?.Dispose();
            _context.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
            await _context.DisposeAsync();
        }

        public IRepository<TAggregate, TKey> GetRepository<TAggregate, TKey>()
            where TAggregate : AggregateRoot<TKey>
        {
            var key = typeof(IRepository<TAggregate, TKey>);
            if (_repositoryCache.TryGetValue(key, out var repository))
            {
                return (IRepository<TAggregate, TKey>)repository;
            }

            var repoInstance = new Repository<TAggregate, TKey>(_context);
            _repositoryCache.TryAdd(key, repoInstance);
            return repoInstance;
        }

        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            return _serviceProvider.GetRequiredService<TRepository>();
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("No active transaction to rollback.");
            }

            await _context.Database.RollbackTransactionAsync(cancellationToken);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
