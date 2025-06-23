using FluentValidation;
using MediatR;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedApplication.Behaviors.Validation
{
    /// <summary>
    /// Pipeline behavior for validating MediatR requests using FluentValidation.
    /// Returns a failure result if validation errors are found.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response, must inherit from <see cref="Result"/>.</typeparam>
    public class ValidationBehavior<TRequest, TResponse>(
        IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : Result
    {
        static readonly Error InvalidRequest = Error.Validation("Request.Invalid", "The request is invalid. Ensure all required fields are properly set.");

        /// <summary>
        /// Handles the request by validating it and returning a failure result if validation errors are present.
        /// </summary>
        /// <param name="request">The request instance.</param>
        /// <param name="next">The next handler in the pipeline.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The response from the next handler, or a failure result if validation fails.</returns>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!validators.Any())
            {
                return await next(cancellationToken);
            }

            var validationResults = validators
                .Select(validator => validator.Validate(request));
            var validationErrors = validationResults
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure is not null)
                .Select(failure => new
                {
                    Field = failure.PropertyName,
                    Description = failure.ErrorMessage
                })
                .Distinct()
                .ToArray();
            var error = Error.Validation(InvalidRequest.Code,
                InvalidRequest.Description,
                validationErrors);

            if (validationResults.Any(vr => !vr.IsValid) && validationErrors.Any())
            {
                return (TResponse)Result.Failure(error);
            }

            return await next(cancellationToken);
        }
    }
}
