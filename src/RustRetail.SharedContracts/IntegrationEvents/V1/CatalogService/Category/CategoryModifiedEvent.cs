namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Category
{
    public sealed class CategoryModifiedEvent : BaseCategoryEvent
    {
        public CategoryModifiedEvent(Guid categoryId) : base(categoryId)
        {
        }
    }
}
