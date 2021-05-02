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

        public ProductRepository()
        {
            
        }

        public ProductRepository(StoreContext context)
        {
            _context = context;
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

        public void Add(Product product)
        {
            _context.Set<Product>().Add(product);
        }

        public void Update(Product product)
        {
            _context.Set<Product>().Attach(product);
            _context.Entry(product).State = EntityState.Modified;
        }

        public void Delete(Product product)
        {
            _context.Set<Product>().Remove(product);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var query = GetProductsQuery();
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(IRepositoryParameters repositoryParameters)
        {
            var productRepositoryObject = (ProductRepositoryObject)repositoryParameters;
            var productsQuery = GetProductsQuery(productRepositoryObject.BrandId, productRepositoryObject.TypeId, productRepositoryObject.SortBy, productRepositoryObject.SortOrder, productRepositoryObject.Skip, productRepositoryObject.Take, productRepositoryObject.IsPagingEnabled, productRepositoryObject.Search);
            return await productsQuery.ToListAsync();
        }

        public async Task<int> CountAsync(IRepositoryParameters repositoryParameters)
        {
            var productRepositoryObject = (ProductRepositoryObject)repositoryParameters;
            var productsQuery = GetProductsQuery(productRepositoryObject.BrandId, productRepositoryObject.TypeId, productRepositoryObject.SortBy, productRepositoryObject.SortOrder, productRepositoryObject.Skip, productRepositoryObject.Take, false, productRepositoryObject.Search);
            return await productsQuery.CountAsync();
        }
    }
}