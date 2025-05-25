using Microsoft.EntityFrameworkCore;
using RustRetail.SharedKernel.Domain.Abstractions;
using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.SharedPersistence.Database
{
    public static class SpecificationEvaluator
    {
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

        private static bool IsPageSizeValid(int? pageSize)
            => pageSize.HasValue && pageSize.Value > 0;

        private static bool IsPageNumberValid(int? pageNumber)
            => pageNumber.HasValue && pageNumber.Value > 0;
    }
}
