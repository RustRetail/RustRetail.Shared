namespace RustRetail.SharedKernel.Domain.Abstractions
{
    public interface IHasConcurrencyToken
    {
        byte[] RowVersion { get; set; }
    }
}
