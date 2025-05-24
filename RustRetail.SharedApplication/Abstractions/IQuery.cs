using MediatR;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedApplication.Abstractions
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
}
