namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Product
{
    public sealed class ProductEnabledEvent : ProductEvent
    {
        public ProductEnabledEvent(Guid productId) : base(productId)
        {
        }
    }
}
