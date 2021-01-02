using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using core.Enumerations;
using core.Interfaces;
using core.RepositoryObjects;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            var brands = await _context.ProductBrands.ToListAsync();
            return brands;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productQuery = GetProductsQuery();
            return await productQuery.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductRepositoryObject productRepositoryObject)
        {
            var productsQuery = GetProductsQuery(productRepositoryObject.BrandId, productRepositoryObject.TypeId, productRepositoryObject.SortBy, productRepositoryObject.SortOrder, productRepositoryObject.Skip, productRepositoryObject.Take, productRepositoryObject.IsPagingEnabled, productRepositoryObject.Search);
            return await productsQuery.ToListAsync();
        }

        public async Task<int> CountAsync(ProductRepositoryObject productRepositoryObject)
        {
            var productsQuery = GetProductsQuery(productRepositoryObject.BrandId, productRepositoryObject.TypeId, productRepositoryObject.SortBy, productRepositoryObject.SortOrder, productRepositoryObject.Skip, productRepositoryObject.Take, false, productRepositoryObject.Search);
            return await productsQuery.CountAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            var types = await _context.ProductTypes.ToListAsync();
            return types;
        }

        private IQueryable<Product> GetProductsQuery(int? brandId = null, int? typeId = null, ProductSortByEnum? productSortBy = null, SortOrderEnum? sortOrderEnum = SortOrderEnum.Ascending, int skip = 0, int take = 0, bool isPagingEnabled = false, string search = "")
        {
            var query = _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .AsQueryable();

            if (brandId.HasValue)
                query = query.Where(x => x.ProductBrandId == brandId);

            if (typeId.HasValue)
                query = query.Where(x => x.ProductTypeId == typeId);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Name.Contains(search));

            if (!productSortBy.HasValue)
                return query;

            if (sortOrderEnum == SortOrderEnum.Ascending)
            {
                switch (productSortBy)
                {
                    case ProductSortByEnum.Name:
                        query = query.OrderBy(o => o.Name);
                        break;
                    case ProductSortByEnum.Price:
                        query = query.OrderBy(o => o.Price);
                        break;
                }
            }
            else
            {
                switch (productSortBy)
                {
                    case ProductSortByEnum.Name:
                        query = query.OrderByDescending(o => o.Name);
                        break;
                    case ProductSortByEnum.Price:
                        query = query.OrderByDescending(o => o.Price);
                        break;
                }
            }

            if (isPagingEnabled)
                query = query.Skip(take * (skip - 1)).Take(take);

            return query;
        }
    }
}