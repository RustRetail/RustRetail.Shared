using RustRetail.SharedKernel.Domain.Models;
using System.Linq.Expressions;

namespace RustRetail.SharedKernel.Domain.Abstractions
{
    /// <summary>
    /// Represents a base class for defining query specifications for aggregate roots.
    /// </summary>
    /// <typeparam name="TAggregate">The aggregate root type.</typeparam>
    /// <typeparam name="TKey">The type of the aggregate key.</typeparam>
    public abstract class Specification<TAggregate, TKey>
        where TAggregate : AggregateRoot<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Specification{TAggregate, TKey}"/> class.
        /// </summary>
        /// <param name="criteria">The filter expression for the specification.</param>
        protected Specification(Expression<Func<TAggregate, bool>>? criteria)
        {
            Criteria = criteria;
        }

        /// <summary>
        /// Gets the filter expression for the specification.
        /// </summary>
        public Expression<Func<TAggregate, bool>>? Criteria { get; }

        /// <summary>
        /// Gets the list of single-level, type-safe include expressions.
        /// </summary>
        public List<Expression<Func<TAggregate, object>>> IncludeExpressions { get; } = new();

        /// <summary>
        /// Gets the list of multi-level, string-based include paths.
        /// </summary>
        public List<string> IncludeStrings { get; } = new();

        /// <summary>
        /// Gets the list of order expressions and their sort direction.
        /// </summary>
        public List<(Expression<Func<TAggregate, object>>, bool isDescending)> OrderExpressions { get; } = new();

        /// <summary>
        /// Gets a value indicating whether split queries are enabled.
        /// </summary>
        public bool IsSplitQuery { get; protected set; } = false;

        /// <summary>
        /// Gets the page number for pagination, if applied.
        /// </summary>
        public int? PageNumber { get; private set; }

        /// <summary>
        /// Gets the page size for pagination, if applied.
        /// </summary>
        public int? PageSize { get; private set; }

        /// <summary>
        /// Gets a value indicating whether tracking is enabled.
        /// </summary>
        public bool AsTracking { get; protected set; } = true;

        /// <summary>
        /// Adds a type-safe include expression for eager loading.
        /// </summary>
        /// <param name="includeExpression">The include expression.</param>
        protected void AddInclude(Expression<Func<TAggregate, object>> includeExpression)
            => IncludeExpressions.Add(includeExpression);

        /// <summary>
        /// Adds a string-based include path for eager loading.
        /// </summary>
        /// <param name="includeString">The include path.</param>
        protected void AddInclude(string includeString)
            => IncludeStrings.Add(includeString);

        /// <summary>
        /// Adds an ascending order expression.
        /// </summary>
        /// <param name="orderByExpression">The order by expression.</param>
        protected void AddOrderBy(Expression<Func<TAggregate, object>> orderByExpression)
            => OrderExpressions.Add((orderByExpression, false));

        /// <summary>
        /// Adds a descending order expression.
        /// </summary>
        /// <param name="orderByDescendingExpression">The order by descending expression.</param>
        protected void AddOrderByDescending(Expression<Func<TAggregate, object>> orderByDescendingExpression)
            => OrderExpressions.Add((orderByDescendingExpression, true));

        /// <summary>
        /// Applies pagination to the specification.
        /// </summary>
        /// <param name="pageNumber">The page number (must be greater than zero).</param>
        /// <param name="pageSize">The page size (must be greater than zero).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if page number or page size is not greater than zero.</exception>
        protected void ApplyPagination(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than zero.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        /// <summary>
        /// Enables split query execution.
        /// </summary>
        protected void EnableSplitQuery()
            => IsSplitQuery = true;

        /// <summary>
        /// Disables change tracking for the query.
        /// </summary>
        protected void DisableTracking()
            => AsTracking = false;

        /// <summary>
        /// Enables change tracking for the query.
        /// </summary>
        protected void EnableTracking()
            => AsTracking = true;
    }
}
