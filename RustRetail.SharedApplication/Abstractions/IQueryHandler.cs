using MediatR;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedApplication.Abstractions
{
    public interface IQueryHandler<in TQuery, TResponse>
        : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
