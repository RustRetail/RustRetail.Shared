using RustRetail.SharedKernel.Domain.Enums;

namespace RustRetail.SharedKernel.Domain.Abstractions
{
    public record Error
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorType Type { get; }
        public object? Value { get; }

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

        public static Error None = new(string.Empty, string.Empty, ErrorType.None);
        public static Error NotFound(string code, string description, object? value = null) =>
            new(code, description, ErrorType.NotFound, value);
        public static Error Validation(string code, string description, object? value = null) =>
            new(code, description, ErrorType.Validation, value);
        public static Error Conflict(string code, string description, object? value = null) =>
            new(code, description, ErrorType.Conflict, value);
        public static Error Failure(string code, string description, object? value = null) =>
            new(code, description, ErrorType.Failure, value);
        public static Error Unauthorized(string code, string description, object? value = null) =>
            new(code, description, ErrorType.Unauthorized, value);
        public static Error Forbidden(string code, string description, object? value = null) =>
            new(code, description, ErrorType.Forbidden, value);
        public static Error ErrorWithValue(Error error, object? value = null) =>
            new(error.Code, error.Description, error.Type, value);

        public override string ToString()
        {
            return $"[{Type.Name}] {Code}: {Description}" + (Value is not null ? $" (Value: {Value})" : "");
        }
    }
}
