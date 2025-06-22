using MediatR;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedApplication.Abstractions
{
    /// <summary>
    /// Defines a handler for processing command requests that return a <see cref="Result"/>.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command request.</typeparam>
    public interface ICommandHandler<in TCommand>
        : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }

    /// <summary>
    /// Defines a handler for processing command requests that return a <see cref="Result{TResponse}"/>.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command request.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the command.</typeparam>
    public interface ICommandHandler<in TCommand, TResponse>
        : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    {
    }
}
