namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Brand
{
    public sealed class BrandCreatedEvent : BaseBrandEvent
    {
        public BrandCreatedEvent(Guid brandId) : base(brandId)
        {
        }
    }
}
