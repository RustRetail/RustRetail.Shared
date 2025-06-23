using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using RustRetail.SharedKernel.Domain.Abstractions;
using RustRetail.SharedKernel.Domain.Models;
using System.Collections.Concurrent;

namespace RustRetail.SharedPersistence.Database
{
    /// <summary>
    /// Provides a base implementation of the unit of work pattern for Entity Framework Core.
    /// Manages transactions, repository access, and coordinates changes across multiple repositories.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    public abstract class UnitOfWork<TDbContext>
        : IUnitOfWork
        where TDbContext : DbContext
    {
        /// <summary>
        /// The underlying database context.
        /// </summary>
        protected readonly TDbContext _context;
        /// <summary>
        /// The service provider for resolving custom repositories.
        /// </summary>
        protected readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// The current database transaction, if any.
        /// </summary>
        protected IDbContextTransaction? _currentTransaction;
        /// <summary>
        /// Cache for generic repository instances.
        /// </summary>
        protected readonly ConcurrentDictionary<Type, object> _repositoryCache = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TDbContext}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="serviceProvider">The service provider for resolving repositories.</param>
        public UnitOfWork(TDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public bool HasActiveTransaction => _currentTransaction != null;

        /// <inheritdoc/>
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already active. Please commit or rollback the current transaction before starting a new one.");
            }
            _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void Dispose()
        {
            _currentTransaction?.Dispose();
            _context.Dispose();
        }

        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
            await _context.DisposeAsync();
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            return _serviceProvider.GetRequiredService<TRepository>();
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
