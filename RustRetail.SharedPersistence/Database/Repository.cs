using Microsoft.EntityFrameworkCore;
using RustRetail.SharedKernel.Domain.Abstractions;
using RustRetail.SharedKernel.Domain.Models;
using System.Linq.Expressions;

namespace RustRetail.SharedPersistence.Database
{
    public class Repository<TAggregate, TKey>
        : IRepository<TAggregate, TKey>
        where TAggregate : AggregateRoot<TKey>
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TAggregate> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TAggregate>();
        }

        public async Task AddAsync(TAggregate entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public void BulkDelete(IEnumerable<TAggregate> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task BulkInsertAsync(IEnumerable<TAggregate> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public void BulkUpdate(IEnumerable<TAggregate> entities)
        {
            _context.UpdateRange(entities);
        }

        public async Task<int> CountAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(predicate, cancellationToken);
        }

        public void Delete(TAggregate entity)
        {
            _context.Remove(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }

        public async Task<List<TAggregate>> FindAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<TAggregate>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<TAggregate?> GetByIdAsync(TKey id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TAggregate> query = _dbSet;
            if (asNoTracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(e => e.Id!.Equals(id), cancellationToken);
        }

        public async Task<T?> QueryAsync<T>(Func<IQueryable<TAggregate>, IQueryable<T>> queryFunc, CancellationToken cancellationToken = default)
        {
            return await queryFunc(_dbSet.AsQueryable())
                .FirstOrDefaultAsync(cancellationToken);
        }

        public void Update(TAggregate entity)
        {
            _context.Update(entity);
        }
    }
}
