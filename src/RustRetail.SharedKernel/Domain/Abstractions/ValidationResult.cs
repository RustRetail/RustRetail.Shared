namespace RustRetail.SharedKernel.Domain.Abstractions
{
    /// <summary>
    /// Represents the result of a validation operation.
    /// </summary>
    public class ValidationResult
        : Result, IValidationResult
    {
        /// <summary>
        /// Gets the collection of validation errors.
        /// </summary>
        public Error[] ValidationErrors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class with the specified errors.
        /// </summary>
        /// <param name="errors">The validation errors.</param>
        private ValidationResult(Error[] errors) : base(false, [IValidationResult.ValidationError])
            => ValidationErrors = errors;

        /// <summary>
        /// Creates a <see cref="ValidationResult"/> with the specified errors.
        /// </summary>
        /// <param name="errors">The validation errors.</param>
        /// <returns>A new <see cref="ValidationResult"/> instance.</returns>
        public static ValidationResult WithErrors(Error[] errors) => new(errors);
    }

    /// <summary>
    /// Represents the result of a validation operation that returns a value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value returned by the operation.</typeparam>
    public class ValidationResult<TValue>
        : Result<TValue>, IValidationResult
    {
        /// <summary>
        /// Gets the collection of validation errors.
        /// </summary>
        public Error[] ValidationErrors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult{TValue}"/> class with the specified errors.
        /// </summary>
        /// <param name="errors">The validation errors.</param>
        private ValidationResult(Error[] errors) : base(default, false, [IValidationResult.ValidationError])
            => ValidationErrors = errors;

        /// <summary>
        /// Creates a <see cref="ValidationResult{TValue}"/> with the specified errors.
        /// </summary>
        /// <param name="errors">The validation errors.</param>
        /// <returns>A new <see cref="ValidationResult{TValue}"/> instance.</returns>
        public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
    }
}
