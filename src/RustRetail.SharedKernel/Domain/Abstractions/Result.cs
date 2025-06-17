namespace RustRetail.SharedKernel.Domain.Abstractions
{
    /// <summary>
    /// Represents the result of an operation, indicating success or failure and containing error information if applicable.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; }
        /// <summary>
        /// Gets the errors associated with a failed operation, or <c>null</c> if successful.
        /// </summary>
        public Error[]? Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="isSuccess">Indicates if the result is successful.</param>
        /// <param name="errors">The errors associated with the result, if any.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if a successful result contains errors, or a failed result contains no errors.
        /// </exception>
        protected internal Result(bool isSuccess, Error[]? errors = null)
        {
            if (isSuccess && errors != null && errors.Length > 0)
                throw new InvalidOperationException("Cannot create successful result with error(s).");

            if (!isSuccess && (errors == null || errors.Length == 0))
                throw new InvalidOperationException("Cannot create failure result with no error.");

            IsSuccess = isSuccess;
            Errors = errors;
        }

        /// <summary>
        /// Creates a successful result.
        /// </summary>
        public static Result Success() => new Result(true);

        /// <summary>
        /// Creates a failed result with a single error.
        /// </summary>
        /// <param name="error">The error associated with the failure.</param>
        public static Result Failure(Error error) => new Result(false, [error]);

        /// <summary>
        /// Creates a failed result with multiple errors.
        /// </summary>
        /// <param name="errors">The errors associated with the failure.</param>
        public static Result Failure(Error[] errors) => new Result(false, errors);

        /// <summary>
        /// Creates a successful result with a value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value of the successful result.</param>
        public static Result<TValue> Success<TValue>(TValue? value) => new Result<TValue>(value, true);

        /// <summary>
        /// Creates a failed result with a single error and a value type.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="error">The error associated with the failure.</param>
        public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default, false, [error]);

        /// <summary>
        /// Creates a failed result with multiple errors and a value type.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="errors">The errors associated with the failure.</param>
        public static Result<TValue> Failure<TValue>(Error[] errors) => new Result<TValue>(default, false, errors);

        /// <summary>
        /// Deconstructs the result into its success state and errors.
        /// </summary>
        /// <param name="isSuccess">Indicates if the result is successful.</param>
        /// <param name="errors">The errors associated with the result.</param>
        public void Deconstruct(out bool isSuccess, out Error[]? errors)
            => (isSuccess, errors) = (IsSuccess, Errors);

        /// <summary>
        /// Returns a string representation of the result.
        /// </summary>
        /// <returns>A string describing the result.</returns>
        public override string ToString()
            => IsSuccess
                ? "Success"
                : $"Failure: {string.Join(", ", Errors?.Select(e => e.Code) ?? Array.Empty<string>())}";
    }

    /// <summary>
    /// Represents the result of an operation that returns a value, indicating success or failure and containing error information if applicable.
    /// </summary>
    /// <typeparam name="T">The type of the value returned by the operation.</typeparam>
    public class Result<T> : Result
    {
        private readonly T? _value;

        /// <summary>
        /// Gets the value of a successful result.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if accessed on a failed result.</exception>
        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Value of failure result cannot be accessed.");

        /// <summary>
        /// Attempts to get the value if the result is successful.
        /// </summary>
        /// <param name="value">The value if successful; otherwise, the default value.</param>
        /// <returns><c>true</c> if the result is successful; otherwise, <c>false</c>.</returns>
        public bool TryGetValue(out T? value)
        {
            value = IsSuccess ? _value : default;
            return IsSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="value">The value of the result.</param>
        /// <param name="isSuccess">Indicates if the result is successful.</param>
        /// <param name="errors">The errors associated with the result, if any.</param>
        protected internal Result(T? value, bool isSuccess, Error[]? errors = null)
            : base(isSuccess, errors)
        {
            _value = value;
        }
    }
}
