namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Product
{
    public sealed class ProductModifiedEvent : ProductEvent
    {
        public ProductModifiedEvent(Guid productId) : base(productId)
        {
        }
    }
}
