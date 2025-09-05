namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Product
{
    public sealed class ProductDeletedEvent : BaseProductEvent
    {
        public ProductDeletedEvent(Guid productId) : base(productId)
        {
        }
    }
}
