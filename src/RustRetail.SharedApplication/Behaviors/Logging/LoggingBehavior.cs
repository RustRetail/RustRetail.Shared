using MediatR;
using Microsoft.Extensions.Logging;
using RustRetail.SharedKernel.Domain.Abstractions;
using System.Diagnostics;

namespace RustRetail.SharedApplication.Behaviors.Logging
{
    /// <summary>
    /// Pipeline behavior for logging the lifecycle and outcome of MediatR requests.
    /// Logs request start, completion, elapsed time, and errors if any.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response, must inherit from <see cref="Result"/>.</typeparam>
    public class LoggingBehavior<TRequest, TResponse>(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : Result
    {
        /// <summary>
        /// Handles the request, logging its execution details and errors.
        /// </summary>
        /// <param name="request">The request instance.</param>
        /// <param name="next">The next handler in the pipeline.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The response from the next handler.</returns>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting request {@RequestName} - TraceId: {TraceId} - Timestamp: {@DateTimeUtc}",
                typeof(TRequest).Name,
                Activity.Current?.Id ?? "N/A",
                DateTime.UtcNow);

            var stopwatch = Stopwatch.StartNew();
            var result = await next(cancellationToken);
            stopwatch.Stop();

            if (!result.IsSuccess)
            {
                logger.LogError("Request failure {@RequestName} - Error(s): {@Error} - Timestamp: {@DateTimeUtc}",
                    typeof(TRequest).Name,
                    result.Errors,
                    DateTime.UtcNow);
            }

            logger.LogInformation("Completed request {@RequestName} - Time elapsed: {ElapsedMilliseconds} ms - Timestamp: {@DateTimeUtc}",
                typeof(TRequest).Name,
                stopwatch.ElapsedMilliseconds,
                DateTime.UtcNow);
            return result;
        }
    }
}
