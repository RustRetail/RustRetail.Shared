namespace RustRetail.SharedKernel.Domain.Common
{
    public class PagedList<T> where T : class
    {
        public List<T> Items { get; } = new List<T>();

        public int PageNumber { get; }

        public int PageSize { get; }

        public int TotalCount { get; set; }

        public bool HasNextPage => PageNumber * PageSize < TotalCount;

        public bool HasPreviousPage => PageNumber > 1;

        private PagedList(
            List<T> items,
            int pageNumber,
            int pageSize,
            int totalCount)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public static PagedList<T> Create(
            IQueryable<T> query,
            int pageNumber,
            int pageSize)
        {
            ValidatePageNavigations(pageNumber, pageSize);

            int totalCount = query.Count();
            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new(items, pageNumber, pageSize, totalCount);
        }

        public static PagedList<T> Create(
            List<T> items,
            int pageNumber,
            int pageSize,
            int totalCount)
        {
            ValidatePageNavigations(pageNumber, pageSize);
            ValidateItemsAndTotalCount(items, totalCount);

            return new(items, pageNumber, pageSize, totalCount);
        }

        private static void ValidatePageNavigations(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentException("Page number must be larger than 0.");
            }
            if (pageSize <= 0)
            {
                throw new ArgumentException("Page size must be larger than 0.");
            }
        }

        private static void ValidateItemsAndTotalCount(List<T> items, int totalCount)
        {
            if (totalCount < 0)
            {
                throw new ArgumentException("Total item count must be equal or larger than 0.");
            }
            if (items != null)
            {
                if (items.Count > 0 && totalCount <= 0)
                {
                    throw new InvalidOperationException("Total item count must be larger than 0 when the items list contains element(s).");
                }
                if (items.Count == 0 && totalCount > 0)
                {
                    throw new InvalidOperationException("Total item count must be 0 when the items list contains no element.");
                }
            }
            else
            {
                if (totalCount > 0)
                {
                    throw new ArgumentException("Total item count must be 0 when the item list is null.");
                }
            }
        }
    }
}
