namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Product
{
    public sealed class ProductDeletedEvent : ProductEvent
    {
        public ProductDeletedEvent(Guid productId) : base(productId)
        {
        }
    }
}
