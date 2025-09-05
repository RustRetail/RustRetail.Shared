using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Product
{
    /// <summary>
    /// Generic integration event for product aggregate.
    /// </summary>
    public abstract class BaseProductEvent : IntegrationEvent
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        public Guid ProductId { get; init; }

        public BaseProductEvent(Guid productId)
        {
            ProductId = productId;
        }
    }
}
