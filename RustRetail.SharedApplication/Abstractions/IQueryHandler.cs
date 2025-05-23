using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedApplication.Abstractions
{
    public interface IQueryHandler<in TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken = default);
    }
}
