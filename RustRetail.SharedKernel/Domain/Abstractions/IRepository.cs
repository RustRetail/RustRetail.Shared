using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.SharedKernel.Domain.Abstractions
{
    public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        IQueryable<TEntity> GetQueryableSet();

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default);

        Task<T?> SingleOrDefaultAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default);

        Task<List<T>> ToListAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default);

        Task BulkInsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        void BulkUpdate(IEnumerable<TEntity> entities);

        void BulkDelete(IEnumerable<TEntity> entities);
    }
}
