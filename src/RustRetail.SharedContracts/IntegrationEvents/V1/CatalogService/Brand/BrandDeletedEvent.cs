namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Brand
{
    public sealed class BrandDeletedEvent : BaseBrandEvent
    {
        public BrandDeletedEvent(Guid brandId) : base(brandId)
        {
        }
    }
}
