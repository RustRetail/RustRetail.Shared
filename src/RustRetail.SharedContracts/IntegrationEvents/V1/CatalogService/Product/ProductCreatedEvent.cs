namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Product
{
    public sealed class ProductCreatedEvent : BaseProductEvent
    {
        public ProductCreatedEvent(Guid productId) : base(productId)
        {
        }
    }
}
