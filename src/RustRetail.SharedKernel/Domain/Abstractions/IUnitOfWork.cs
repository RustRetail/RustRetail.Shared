using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.SharedKernel.Domain.Abstractions
{
    /// <summary>
    /// Defines a contract for coordinating changes across multiple repositories with transaction support.
    /// </summary>
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Indicates whether there is an active database transaction.
        /// </summary>
        bool HasActiveTransaction { get; }

        /// <summary>
        /// Gets a generic repository for the specified aggregate root type.
        /// </summary>
        /// <typeparam name="TAggregate">The aggregate root type.</typeparam>
        /// <typeparam name="TKey">The type of the aggregate root's key.</typeparam>
        /// <returns>An instance of <see cref="IRepository{TAggregate, TKey}"/> for the given aggregate type.</returns>
        IRepository<TAggregate, TKey> GetRepository<TAggregate, TKey>() where TAggregate : AggregateRoot<TKey>;

        /// <summary>
        /// Gets a custom repository implementation that has been registered in the dependency injection container.
        /// </summary>
        /// <typeparam name="TRepository">The type of the custom repository interface or class.</typeparam>
        /// <returns>An instance of the custom repository.</returns>
        TRepository GetRepository<TRepository>() where TRepository : class;

        /// <summary>
        /// Saves all changes made in this unit of work to the database.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commits the current database transaction.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Rolls back the current database transaction.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
