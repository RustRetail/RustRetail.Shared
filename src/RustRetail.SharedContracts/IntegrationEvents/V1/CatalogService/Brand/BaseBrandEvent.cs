using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Brand
{
    /// <summary>
    /// Generic integration event for brand aggregate.
    /// </summary>
    public abstract class BaseBrandEvent : IntegrationEvent
    {
        /// <summary>
        /// Brand's identifier
        /// </summary>
        public Guid BrandId { get; init; }

        protected BaseBrandEvent(Guid brandId)
        {
            BrandId = brandId;
        }
    }
}
