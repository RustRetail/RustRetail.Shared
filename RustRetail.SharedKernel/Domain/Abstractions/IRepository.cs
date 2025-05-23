using RustRetail.SharedKernel.Domain.Models;
using System.Linq.Expressions;

namespace RustRetail.SharedKernel.Domain.Abstractions
{
    public interface IRepository<TAggregate, TKey> where TAggregate : AggregateRoot<TKey>
    {
        Task AddAsync(TAggregate entity, CancellationToken cancellationToken = default);

        void Update(TAggregate entity);

        void Delete(TAggregate entity);

        Task BulkInsertAsync(IEnumerable<TAggregate> entities, CancellationToken cancellationToken = default);

        void BulkUpdate(IEnumerable<TAggregate> entities);

        void BulkDelete(IEnumerable<TAggregate> entities);

        Task<List<TAggregate>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<int> CountAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default);

        Task<T?> QueryAsync<T>(Func<IQueryable<TAggregate>, IQueryable<T>> queryFunc, CancellationToken cancellationToken = default);

        Task<TAggregate?> GetByIdAsync(TKey id, bool asNoTracking = false, CancellationToken cancellationToken = default);

        Task<List<TAggregate>> FindAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
