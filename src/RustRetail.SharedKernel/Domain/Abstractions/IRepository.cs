using RustRetail.SharedKernel.Domain.Models;
using System.Linq.Expressions;

namespace RustRetail.SharedKernel.Domain.Abstractions
{
    /// <summary>
    /// Defines a contract for repository operations on aggregate roots.
    /// </summary>
    /// <typeparam name="TAggregate">The aggregate root type.</typeparam>
    /// <typeparam name="TKey">The type of the aggregate key.</typeparam>
    public interface IRepository<TAggregate, TKey> where TAggregate : AggregateRoot<TKey>
    {
        /// <summary>
        /// Adds a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        Task AddAsync(TAggregate entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(TAggregate entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TAggregate entity);

        /// <summary>
        /// Inserts multiple entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        Task BulkInsertAsync(IEnumerable<TAggregate> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates multiple entities.
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        void BulkUpdate(IEnumerable<TAggregate> entities);

        /// <summary>
        /// Deletes multiple entities.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        void BulkDelete(IEnumerable<TAggregate> entities);

        /// <summary>
        /// Gets all entities asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A list of all entities.</returns>
        Task<List<TAggregate>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Counts entities matching a predicate asynchronously.
        /// </summary>
        /// <param name="predicate">The filter expression.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The number of matching entities.</returns>
        Task<int> CountAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a custom query asynchronously.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="queryFunc">A function to project the query.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The result of the query.</returns>
        Task<T?> QueryAsync<T>(Func<IQueryable<TAggregate>, IQueryable<T>> queryFunc, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an entity by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        /// <param name="asNoTracking">Whether to disable change tracking.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The entity if found; otherwise, <c>null</c>.</returns>
        Task<TAggregate?> GetByIdAsync(TKey id, bool asNoTracking = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds entities matching a predicate asynchronously.
        /// </summary>
        /// <param name="predicate">The filter expression.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A list of matching entities.</returns>
        Task<List<TAggregate>> FindAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Determines whether any entities match a predicate asynchronously.
        /// </summary>
        /// <param name="predicate">The filter expression.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns><c>true</c> if any entities match; otherwise, <c>false</c>.</returns>
        Task<bool> ExistsAsync(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
