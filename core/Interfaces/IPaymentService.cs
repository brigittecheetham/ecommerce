using System.Threading.Tasks;
using core.Entities;

namespace core.Interfaces
{
    public interface IPaymentService
    {
         Task<CustomerBasket>CreateOrUpdatePaymentIntent(string basketId);
    }
}