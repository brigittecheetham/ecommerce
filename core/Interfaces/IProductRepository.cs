using System.Collections.Generic;
using System.Threading.Tasks;
using core.Entities;
using core.RepositoryObjects;

namespace core.Interfaces
{
    public interface IProductRepository
    {
         Task<Product> GetProductByIdAsync(int id);
         Task<IReadOnlyList<Product>> GetProductsAsync(ProductRepositoryObject productRepositoryObject);
         Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
         Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
         Task<int> CountAsync(ProductRepositoryObject productRepositoryObject);
    }
}