using RustRetail.SharedKernel.Domain.Enums;

namespace RustRetail.SharedKernel.Domain.Abstractions
{
    /// <summary>
    /// Represents a domain error with code, description, type, and optional value.
    /// </summary>
    public record Error
    {
        /// <summary>
        /// Gets the error code.
        /// </summary>
        public string Code { get; }
        /// <summary>
        /// Gets the error description.
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// Gets the error type.
        /// </summary>
        public ErrorType Type { get; }
        /// <summary>
        /// Gets the optional value associated with the error.
        /// </summary>
        public object? Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> record.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="type">The error type.</param>
        /// <param name="value">The optional value.</param>
        private Error(
            string code,
            string description,
            ErrorType type,
            object? value = null)
        {
            Code = code;
            Description = description;
            Type = type;
            Value = value;
        }

        /// <summary>
        /// Represents no error.
        /// </summary>
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);
        /// <summary>
        /// Creates a not found error.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="value">The optional value.</param>
        /// <returns>A not found <see cref="Error"/>.</returns>
        public static Error NotFound(string code, string description, object? value = null) =>
            new(code, description, ErrorType.NotFound, value);
        /// <summary>
        /// Creates a validation error.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="value">The optional value.</param>
        /// <returns>A validation <see cref="Error"/>.</returns>
        public static Error Validation(string code, string description, object? value = null) =>
            new(code, description, ErrorType.Validation, value);
        /// <summary>
        /// Creates a conflict error.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="value">The optional value.</param>
        /// <returns>A conflict <see cref="Error"/>.</returns>
        public static Error Conflict(string code, string description, object? value = null) =>
            new(code, description, ErrorType.Conflict, value);
        /// <summary>
        /// Creates a failure error.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="value">The optional value.</param>
        /// <returns>A failure <see cref="Error"/>.</returns>
        public static Error Failure(string code, string description, object? value = null) =>
            new(code, description, ErrorType.Failure, value);
        /// <summary>
        /// Creates an unauthorized error.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="value">The optional value.</param>
        /// <returns>An unauthorized <see cref="Error"/>.</returns>
        public static Error Unauthorized(string code, string description, object? value = null) =>
            new(code, description, ErrorType.Unauthorized, value);
        /// <summary>
        /// Creates a forbidden error.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="value">The optional value.</param>
        /// <returns>A forbidden <see cref="Error"/>.</returns>
        public static Error Forbidden(string code, string description, object? value = null) =>
            new(code, description, ErrorType.Forbidden, value);
        /// <summary>
        /// Creates a new error with the same code, description, and type, but a different value.
        /// </summary>
        /// <param name="error">The base error.</param>
        /// <param name="value">The new value.</param>
        /// <returns>A new <see cref="Error"/> with the specified value.</returns>
        public static Error ErrorWithValue(Error error, object? value = null) =>
            new(error.Code, error.Description, error.Type, value);

        /// <summary>
        /// Returns a string representation of the error.
        /// </summary>
        /// <returns>A string describing the error.</returns>
        public override string ToString()
        {
            return $"[{Type.Name}] {Code}: {Description}" + (Value is not null ? $" (Value: {Value})" : "");
        }
    }
}
