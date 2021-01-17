using System.Threading.Tasks;
using core.Entities;

namespace core.Interfaces
{
    public interface IBasketRepository
    {
         Task<CustomerBasket> GetBasketAsync(string basketId);
         Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket);
         Task<bool> DeleteBasketAsync(string basketId);
    }
}