using Microsoft.EntityFrameworkCore;
using RustRetail.SharedKernel.Domain.Abstractions;
using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.SharedPersistence.Database
{
    /// <summary>
    /// Provides methods to evaluate and apply a <see cref="Specification{TAggregate, TKey}"/> to an <see cref="IQueryable{TAggregate}"/>.
    /// </summary>
    public static class SpecificationEvaluator
    {
        /// <summary>
        /// Applies the given specification to the provided queryable, including filtering, includes, ordering, pagination, split query, and tracking options.
        /// </summary>
        /// <typeparam name="TAggregate">The type of the aggregate root.</typeparam>
        /// <typeparam name="TKey">The type of the aggregate key.</typeparam>
        /// <param name="inputQueryable">The initial queryable to apply the specification to.</param>
        /// <param name="specification">The specification containing query criteria and options.</param>
        /// <returns>
        /// An <see cref="IQueryable{TAggregate}"/> with the specification applied.
        /// </returns>
        public static IQueryable<TAggregate> GetQuery<TAggregate, TKey>(
                IQueryable<TAggregate> inputQueryable,
                Specification<TAggregate, TKey> specification)
                where TAggregate : AggregateRoot<TKey>
        {
            IQueryable<TAggregate> queryable = inputQueryable;

            // Where
            if (specification.Criteria is not null)
            {
                queryable = queryable.Where(specification.Criteria);
            }

            // Include
            foreach (var includeExpression in specification.IncludeExpressions)
            {
                queryable = queryable.Include(includeExpression);
            }
            foreach (string includeString in specification.IncludeStrings)
            {
                queryable = queryable.Include(includeString);
            }

            // OrderBy/OrderByDescending
            if (specification.OrderExpressions.Count > 0)
            {
                bool firstOrdering = true;
                foreach (var (expression, isDescending) in specification.OrderExpressions)
                {
                    if (firstOrdering)
                    {
                        queryable = isDescending
                            ? queryable.OrderByDescending(expression)
                            : queryable.OrderBy(expression);
                        firstOrdering = false;
                    }
                    else
                    {
                        queryable = isDescending
                            ? ((IOrderedQueryable<TAggregate>)queryable).ThenByDescending(expression)
                            : ((IOrderedQueryable<TAggregate>)queryable).ThenBy(expression);
                    }
                }
            }

            // Pagination
            if (IsPageSizeValid(specification.PageSize) &&
                IsPageNumberValid(specification.PageNumber))
            {
                int skip = (specification.PageNumber!.Value - 1) * specification.PageSize!.Value;
                queryable = queryable
                    .Skip(skip)
                    .Take(specification.PageSize.Value);
            }

            // Split Query
            if (specification.IsSplitQuery)
            {
                queryable = queryable.AsSplitQuery();
            }

            // Tracking
            queryable = specification.AsTracking
                ? queryable.AsTracking()
                : queryable.AsNoTracking();

            return queryable;
        }

        /// <summary>
        /// Determines whether the specified page size is valid (greater than zero).
        /// </summary>
        /// <param name="pageSize">The page size to validate.</param>
        /// <returns><c>true</c> if the page size is valid; otherwise, <c>false</c>.</returns>
        private static bool IsPageSizeValid(int? pageSize)
            => pageSize.HasValue && pageSize.Value > 0;

        /// <summary>
        /// Determines whether the specified page number is valid (greater than zero).
        /// </summary>
        /// <param name="pageNumber">The page number to validate.</param>
        /// <returns><c>true</c> if the page number is valid; otherwise, <c>false</c>.</returns>
        private static bool IsPageNumberValid(int? pageNumber)
            => pageNumber.HasValue && pageNumber.Value > 0;
    }
}
