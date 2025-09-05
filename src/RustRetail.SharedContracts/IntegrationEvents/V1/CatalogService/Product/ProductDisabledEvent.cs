namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Product
{
    public sealed class ProductDisabledEvent : ProductEvent
    {
        public ProductDisabledEvent(Guid productId) : base(productId)
        {
        }
    }
}
