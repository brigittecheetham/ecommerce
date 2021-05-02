using core.Enumerations;

namespace core.RepositoryObjects
{
    public class ProductRepositoryObject : IRepositoryParameters
    {
        public ProductSortByEnum? SortBy { get; set; }
        public SortOrderEnum? SortOrder { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagingEnabled { get; set; }
        public string Search { get; set; }
    }
}