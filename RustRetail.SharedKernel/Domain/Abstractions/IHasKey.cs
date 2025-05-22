namespace RustRetail.SharedKernel.Domain.Abstractions
{
    public interface IHasKey<T>
    {
        T Id { get; set; }
    }
}
