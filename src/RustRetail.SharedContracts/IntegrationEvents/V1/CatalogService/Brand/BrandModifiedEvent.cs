namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Brand
{
    public sealed class BrandModifiedEvent : BaseBrandEvent
    {
        public BrandModifiedEvent(Guid brandId) : base(brandId)
        {
        }
    }
}
