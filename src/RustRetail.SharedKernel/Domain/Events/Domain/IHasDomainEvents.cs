namespace RustRetail.SharedKernel.Domain.Events.Domain
{
    public interface IHasDomainEvents
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
