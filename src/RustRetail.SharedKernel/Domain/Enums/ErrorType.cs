namespace RustRetail.SharedKernel.Domain.Enums
{
    /// <summary>
    /// Represents a domain error type for operation results.
    /// </summary>
    public sealed class ErrorType : Enumeration
    {
        /// <summary>
        /// No error.
        /// </summary>
        public static readonly ErrorType None = new(nameof(None), 0);
        /// <summary>
        /// General failure.
        /// </summary>
        public static readonly ErrorType Failure = new(nameof(Failure), 1);
        /// <summary>
        /// Validation error.
        /// </summary>
        public static readonly ErrorType Validation = new(nameof(Validation), 2);
        /// <summary>
        /// Resource not found.
        /// </summary>
        public static readonly ErrorType NotFound = new(nameof(NotFound), 3);
        /// <summary>
        /// Conflict error.
        /// </summary>
        public static readonly ErrorType Conflict = new(nameof(Conflict), 4);
        /// <summary>
        /// Unauthorized access.
        /// </summary>
        public static readonly ErrorType Unauthorized = new(nameof(Unauthorized), 5);
        /// <summary>
        /// Forbidden access.
        /// </summary>
        public static readonly ErrorType Forbidden = new(nameof(Forbidden), 6);

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorType"/> class.
        /// </summary>
        /// <param name="name">The error type name.</param>
        /// <param name="value">The error type value.</param>
        private ErrorType(string name, int value) : base(name, value) { }
    }
}
