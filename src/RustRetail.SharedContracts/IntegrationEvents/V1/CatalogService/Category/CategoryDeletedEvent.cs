namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Category
{
    public sealed class CategoryDeletedEvent : BaseCategoryEvent
    {
        public CategoryDeletedEvent(Guid categoryId) : base(categoryId)
        {
        }
    }
}
