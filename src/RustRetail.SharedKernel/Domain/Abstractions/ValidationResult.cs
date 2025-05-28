namespace RustRetail.SharedKernel.Domain.Abstractions
{
    public class ValidationResult
        : Result, IValidationResult
    {
        private ValidationResult(Error[] errors) : base(false, [IValidationResult.ValidationError])
            => ValidationErrors = errors;

        public Error[] ValidationErrors { get; }

        public static ValidationResult WithErrors(Error[] errors) => new(errors);
    }

    public class ValidationResult<TValue>
        : Result<TValue>, IValidationResult
    {
        private ValidationResult(Error[] errors) : base(default, false, [IValidationResult.ValidationError])
            => ValidationErrors = errors;

        public Error[] ValidationErrors { get; }

        public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
    }
}
