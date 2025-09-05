namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Product
{
    public sealed class ProductModifiedEvent : BaseProductEvent
    {
        public ProductModifiedEvent(Guid productId) : base(productId)
        {
        }
    }
}
