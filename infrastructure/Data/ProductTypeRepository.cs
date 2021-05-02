using System.Collections.Generic;
using System.Threading.Tasks;
using core.Entities;
using core.Interfaces;
using core.RepositoryObjects;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly StoreContext _context;
        public ProductTypeRepository(StoreContext context)
        {
            _context = context;

        }
        public void Add(ProductType entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CountAsync(IRepositoryParameters repositoryParameters)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(ProductType entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<ProductType>> GetAllAsync(IRepositoryParameters repositoryParameters)
        {
            var types = await _context.ProductTypes.ToListAsync();
            return types;
        }

        public Task<ProductType> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ProductType entity)
        {
            throw new System.NotImplementedException();
        }
    }
}