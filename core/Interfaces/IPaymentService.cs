using System.Threading.Tasks;
using core.Entities;
using core.Entities.OrderAggregate;

namespace core.Interfaces
{
    public interface IPaymentService
    {
         Task<CustomerBasket>CreateOrUpdatePaymentIntent(string basketId);

         Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);
         Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}