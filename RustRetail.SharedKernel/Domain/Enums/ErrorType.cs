namespace RustRetail.SharedKernel.Domain.Enums
{
    public sealed class ErrorType : Enumeration
    {
        public static readonly ErrorType None = new(nameof(None), 0);
        public static readonly ErrorType Failure = new(nameof(Failure), 1);
        public static readonly ErrorType Validation = new(nameof(Validation), 2);
        public static readonly ErrorType NotFound = new(nameof(NotFound), 3);
        public static readonly ErrorType Conflict = new(nameof(Conflict), 4);
        public static readonly ErrorType Unauthorized = new(nameof(Unauthorized), 5);
        public static readonly ErrorType Forbidden = new(nameof(Forbidden), 6);

        private ErrorType(string name, int value) : base(name, value) { }
    }
}
