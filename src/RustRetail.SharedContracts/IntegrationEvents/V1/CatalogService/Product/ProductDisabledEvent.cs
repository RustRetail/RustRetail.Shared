namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Product
{
    public sealed class ProductDisabledEvent : BaseProductEvent
    {
        public ProductDisabledEvent(Guid productId) : base(productId)
        {
        }
    }
}
