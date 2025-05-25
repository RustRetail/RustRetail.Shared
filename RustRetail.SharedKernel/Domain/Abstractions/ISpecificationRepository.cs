using RustRetail.SharedKernel.Domain.Common;
using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.SharedKernel.Domain.Abstractions
{
    public interface ISpecificationRepository<TAggregate, TKey>
        where TAggregate : AggregateRoot<TKey>
    {
        /// <summary>
        /// Gets a single entity matching the provided specification.
        /// Returns null if no entity matches.
        /// </summary>
        Task<TAggregate?> GetAsync(Specification<TAggregate, TKey> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all entities matching the provided specification.
        /// </summary>
        Task<List<TAggregate>> FindAsync(Specification<TAggregate, TKey> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a paged list of entities matching the specification,
        /// including paging metadata.
        /// </summary>
        Task<PagedList<TAggregate>> FindPagedAsync(Specification<TAggregate, TKey> specification, CancellationToken cancellationToken = default);
    }
}
