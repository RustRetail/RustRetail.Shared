namespace RustRetail.SharedKernel.Domain.Abstractions
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error[]? Errors { get; }

        protected internal Result(bool isSuccess, Error[]? errors = null)
        {
            if (isSuccess && errors != null && errors.Length > 0)
                throw new InvalidOperationException("Cannot create successful result with error(s).");

            if (!isSuccess && (errors == null || errors.Length == 0))
                throw new InvalidOperationException("Cannot create failure result with no error.");

            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Success() => new Result(true);

        public static Result Failure(Error error) => new Result(false, [error]);

        public static Result Failure(Error[] errors) => new Result(false, errors);

        public static Result<TValue> Success<TValue>(TValue? value) => new Result<TValue>(value, true);

        public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default, false, [error]);

        public static Result<TValue> Failure<TValue>(Error[] errors) => new Result<TValue>(default, false, errors);

        public void Deconstruct(out bool isSuccess, out Error[]? errors)
            => (isSuccess, errors) = (IsSuccess, Errors);

        public override string ToString()
            => IsSuccess
                ? "Success"
                : $"Failure: {string.Join(", ", Errors?.Select(e => e.Code) ?? Array.Empty<string>())}";
    }

    public class Result<T> : Result
    {
        private readonly T? _value;

        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Value of failure result cannot be accessed.");

        public bool TryGetValue(out T? value)
        {
            value = IsSuccess ? _value : default;
            return IsSuccess;
        }

        protected internal Result(T? value, bool isSuccess, Error[]? errors = null)
            : base(isSuccess, errors)
        {
            _value = value;
        }
    }
}
