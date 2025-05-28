namespace RustRetail.SharedKernel.Domain.Abstractions
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = Error.Validation("ValidationError", "Validation problem(s) occurred");
        Error[] ValidationErrors { get; }
    }
}
