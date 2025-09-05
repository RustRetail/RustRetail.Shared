
namespace RustRetail.SharedContracts.IntegrationEvents.V1.CatalogService.Category
{
    public sealed class CategoryCreatedEvent : BaseCategoryEvent
    {
        public CategoryCreatedEvent(Guid categoryId) : base(categoryId)
        {
        }
    }
}
