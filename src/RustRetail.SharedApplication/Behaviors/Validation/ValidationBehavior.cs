using FluentValidation;
using MediatR;
using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedApplication.Behaviors.Validation
{
    public class ValidationBehavior<TRequest, TResponse>(
        IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : Result
    {
        static Error InvalidRequest = Error.Validation("Request.Invalid", "The request is invalid. Ensure all required fields are properly set.");

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
