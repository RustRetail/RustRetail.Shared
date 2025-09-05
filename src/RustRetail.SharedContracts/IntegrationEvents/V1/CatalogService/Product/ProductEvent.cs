using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Product
{
    /// <summary>
    /// Generic integration event for product aggregate.
    /// </summary>
    public abstract class ProductEvent : IntegrationEvent
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        public Guid ProductId { get; init; }

        public ProductEvent(Guid productId)
        {
            ProductId = productId;
        }
    }
}
