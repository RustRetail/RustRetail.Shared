using RustRetail.SharedContracts.IntegrationEvents.Abstractions;

namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Category
{
    /// <summary>
    /// Generic integration event for category aggregate.
    /// </summary>
    public abstract class BaseCategoryEvent : IntegrationEvent
    {
        /// <summary>
        /// Category's identifier
        /// </summary>
        public Guid CategoryId { get; init; }

        public BaseCategoryEvent(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
