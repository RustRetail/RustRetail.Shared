using MediatR;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedApplication.Abstractions
{
    /// <summary>
    /// Represents a query request that returns a <see cref="Result{TResponse}"/> when handled.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response returned by the query.</typeparam>
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
}
