using RustRetail.SharedKernel.Domain.Common;
using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.SharedKernel.Domain.Abstractions
{
    /// <summary>
    /// Defines a contract for querying aggregates using specifications.
    /// </summary>
    /// <typeparam name="TAggregate">The aggregate root type.</typeparam>
    /// <typeparam name="TKey">The type of the aggregate key.</typeparam>
    public interface ISpecificationRepository<TAggregate, TKey>
        where TAggregate : AggregateRoot<TKey>
    {
        /// <summary>
        /// Gets a single entity matching the provided specification.
        /// Returns <c>null</c> if no entity matches.
        /// </summary>
        /// <param name="specification">The specification to filter entities.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The matching entity or <c>null</c> if not found.</returns>
        Task<TAggregate?> GetAsync(Specification<TAggregate, TKey> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all entities matching the provided specification.
        /// </summary>
        /// <param name="specification">The specification to filter entities.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A list of matching entities.</returns>
        Task<List<TAggregate>> FindAsync(Specification<TAggregate, TKey> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a paged list of entities matching the specification, including paging metadata.
        /// </summary>
        /// <param name="specification">The specification to filter entities.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A paged list of matching entities.</returns>
        Task<PagedList<TAggregate>> FindPagedAsync(Specification<TAggregate, TKey> specification, CancellationToken cancellationToken = default);
    }
}
