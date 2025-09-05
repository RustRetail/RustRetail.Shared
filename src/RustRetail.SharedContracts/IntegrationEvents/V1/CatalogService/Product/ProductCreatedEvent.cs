namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Product
{
    public sealed class ProductCreatedEvent : ProductEvent
    {
        public ProductCreatedEvent(Guid productId) : base(productId)
        {
        }
    }
}
