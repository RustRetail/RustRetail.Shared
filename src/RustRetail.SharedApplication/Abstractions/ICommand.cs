using MediatR;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedApplication.Abstractions
{
    public interface ICommand : IRequest<Result>;

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
}
