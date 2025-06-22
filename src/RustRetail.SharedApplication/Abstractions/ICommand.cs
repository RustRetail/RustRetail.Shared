using MediatR;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedApplication.Abstractions
{
    /// <summary>
    /// Represents a command request that returns a <see cref="Result"/> when handled.
    /// </summary>
    public interface ICommand : IRequest<Result>;

    /// <summary>
    /// Represents a command request that returns a <see cref="Result{TResponse}"/> when handled.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response returned by the command.</typeparam>
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
}
