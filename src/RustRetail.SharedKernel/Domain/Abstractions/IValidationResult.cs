namespace RustRetail.SharedKernel.Domain.Abstractions
{
    /// <summary>
    /// Defines a contract for validation result objects.
    /// </summary>
    public interface IValidationResult
    {
        /// <summary>
        /// Represents a generic validation error.
        /// </summary>
        public static readonly Error ValidationError = Error.Validation("ValidationError", "Validation problem(s) occurred");

        /// <summary>
        /// Gets the collection of validation errors.
        /// </summary>
        Error[] ValidationErrors { get; }
    }
}
