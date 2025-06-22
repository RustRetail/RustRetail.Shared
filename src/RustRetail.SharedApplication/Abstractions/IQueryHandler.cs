using MediatR;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedApplication.Abstractions
{
    /// <summary>
    /// Defines a handler for processing query requests and returning a <see cref="Result{TResponse}"/>.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query request.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the query.</typeparam>
    public interface IQueryHandler<in TQuery, TResponse>
        : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
