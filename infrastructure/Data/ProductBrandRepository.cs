using System.Collections.Generic;
using System.Threading.Tasks;
using core.Entities;
using core.Interfaces;
using core.RepositoryObjects;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data
{
    public class ProductBrandRepository : IProductBrandRepository
    {
        private readonly StoreContext _context;
        public ProductBrandRepository(StoreContext context)
        {
            _context = context;

        }
        public void Add(ProductBrand entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CountAsync(IRepositoryParameters repositoryParameters)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(ProductBrand entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllAsync(IRepositoryParameters repositoryParameters)
        {
            var brands = await _context.ProductBrands.ToListAsync();
            return brands;
        }

        public Task<ProductBrand> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ProductBrand entity)
        {
            throw new System.NotImplementedException();
        }
    }
}