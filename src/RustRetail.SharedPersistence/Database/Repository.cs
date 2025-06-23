using Microsoft.EntityFrameworkCore;
using RustRetail.SharedKernel.Domain.Abstractions;
using RustRetail.SharedKernel.Domain.Common;
using RustRetail.SharedKernel.Domain.Models;
using System.Linq.Expressions;

namespace RustRetail.SharedPersistence.Database
{
    /// <summary>
    /// Provides a generic repository implementation for aggregate roots using Entity Framework Core.
    /// Supports both standard and specification-based queries.
    /// </summary>
    /// <typeparam name="TAggregate">The aggregate root type.</typeparam>
    /// <typeparam name="TKey">The type of the aggregate key.</typeparam>
    public class Repository<TAggregate, TKey>
        : IRepository<TAggregate, TKey>,
        ISpecificationRepository<TAggregate, TKey>
        where TAggregate : AggregateRoot<TKey>
    {
        /// <summary>
        /// The underlying database context.
        /// </summary>
        protected readonly DbContext _context;
        /// <summary>
        /// The DbSet for the aggregate root.
        /// </summary>
        protected readonly DbSet<TAggregate> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TAggregate, TKey}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TAggregate>();
        }

        /// <inheritdoc/>
        public async Task AddAsync(TAggregate entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public void BulkDelete(IEnumerable<TAggregate> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <inheritdoc/>
        public async Task BulkInsertAsync(IEnumerable<TAggregate> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        /// <inheritdoc/>
        public void BulkUpdate(IEnumerable<TAggregate> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        /// <inheritdoc/>
        public async Task<int> CountAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public void Delete(TAggregate entity)
        {
            _dbSet.Remove(entity);
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<List<TAggregate>> FindAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<List<TAggregate>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<TAggregate?> GetAsync(Specification<TAggregate, TKey> specification, CancellationToken cancellationToken = default)
        {
            return await SpecificationEvaluator.GetQuery(_dbSet.AsQueryable(), specification).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<TAggregate?> GetByIdAsync(TKey id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TAggregate> query = _dbSet;
            if (asNoTracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(e => e.Id!.Equals(id), cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<List<TAggregate>> FindAsync(Specification<TAggregate, TKey> specification, CancellationToken cancellationToken = default)
        {
            return await SpecificationEvaluator.GetQuery(_dbSet.AsQueryable(), specification).ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<PagedList<TAggregate>> FindPagedAsync(Specification<TAggregate, TKey> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<TAggregate> query = SpecificationEvaluator.GetQuery(_dbSet.AsQueryable(), specification);
            if (!specification.PageNumber.HasValue || !specification.PageSize.HasValue)
            {
                var allItems = await query.ToListAsync(cancellationToken);
                return PagedList<TAggregate>.Create(allItems, 1, allItems.Count, allItems.Count);
            }

            return await Task.Run(() =>
                PagedList<TAggregate>.Create(query, specification.PageNumber!.Value, specification.PageSize!.Value),
                cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T?> QueryAsync<T>(Func<IQueryable<TAggregate>, IQueryable<T>> queryFunc, CancellationToken cancellationToken = default)
        {
            return await queryFunc(_dbSet.AsQueryable())
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public void Update(TAggregate entity)
        {
            _dbSet.Update(entity);
        }
    }
}
