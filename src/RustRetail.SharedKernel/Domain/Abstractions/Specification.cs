using RustRetail.SharedKernel.Domain.Models;
using System.Linq.Expressions;

namespace RustRetail.SharedKernel.Domain.Abstractions
{
    public abstract class Specification<TAggregate, TKey>
        where TAggregate : AggregateRoot<TKey>
    {
        protected Specification(Expression<Func<TAggregate, bool>>? criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<TAggregate, bool>>? Criteria { get; }

        // Single-level, type-safe includes
        public List<Expression<Func<TAggregate, object>>> IncludeExpressions { get; } = new();

        // Multi-level, string-based includes
        public List<string> IncludeStrings { get; } = new();

        public List<(Expression<Func<TAggregate, object>>, bool isDescending)> OrderExpressions { get; } = new();

        public bool IsSplitQuery { get; protected set; } = false;

        public int? PageNumber { get; private set; }

        public int? PageSize { get; private set; }

        public bool AsTracking { get; protected set; } = true;

        protected void AddInclude(Expression<Func<TAggregate, object>> includeExpression)
            => IncludeExpressions.Add(includeExpression);

        protected void AddInclude(string includeString)
            => IncludeStrings.Add(includeString);

        protected void AddOrderBy(Expression<Func<TAggregate, object>> orderByExpression)
        => OrderExpressions.Add((orderByExpression, false));

        protected void AddOrderByDescending(Expression<Func<TAggregate, object>> orderByDescendingExpression)
            => OrderExpressions.Add((orderByDescendingExpression, true));

        protected void ApplyPagination(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than zero.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        protected void EnableSplitQuery()
            => IsSplitQuery = true;

        protected void DisableTracking()
            => AsTracking = false;

        protected void EnableTracking()
            => AsTracking = true;
    }
}
