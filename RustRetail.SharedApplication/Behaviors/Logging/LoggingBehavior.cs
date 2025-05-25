using MediatR;
using Microsoft.Extensions.Logging;
using RustRetail.SharedKernel.Domain.Abstractions;
using System.Diagnostics;

namespace RustRetail.SharedApplication.Behaviors.Logging
{
    public class LoggingBehavior<TRequest, TResponse>(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : Result
    {
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
