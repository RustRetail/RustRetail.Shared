namespace RustRetail.SharedKernel.Domain.Abstractions
{
    public interface ITrackable
    {
        DateTimeOffset? CreatedDateTime { get; }
        DateTimeOffset? UpdatedDateTime { get; }

        void SetCreatedDateTime(DateTimeOffset? createdDateTime);
        void SetUpdatedDateTime(DateTimeOffset? updatedDateTime);
    }
}
