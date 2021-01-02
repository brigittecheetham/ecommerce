using core.Enumerations;

namespace api.Dtos
{
    public class ProductRequestDto
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 6;
        private string _search;

        public ProductSortByEnum? SortBy { get; set; }
        public SortOrderEnum? SortOrder { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}