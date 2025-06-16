namespace RustRetail.SharedKernel.Domain.Abstractions
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
        DateTimeOffset? DeletedDateTime { get; set; }
    }
}
