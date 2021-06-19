using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using core.Entities.OrderAggregate;
using core.Interfaces;
using core.RepositoryObjects;

namespace infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _paymentService = paymentService;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            var items = new List<OrderItem>();

            foreach (var item in basket.BasketItems)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var orderRepositoryObject = new OrderRepositoryObject
            {
                PaymentIntentId = basket.PaymentIntendId
            };

            var existingOrder = await _unitOfWork.Repository<Order>().GetAllAsync(orderRepositoryObject);

            if (existingOrder.Any())
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder[0]);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.PaymentIntendId);
            }

            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, subtotal, items, basket.PaymentIntendId);
            _unitOfWork.Repository<Order>().Add(order);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;
            
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync(null);
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(id);

            if (order?.BuyerEmail != buyerEmail)
            {
                return null;
            }

            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderRepositoryObject = new OrderRepositoryObject
            {
                BuyerEmail = buyerEmail
            };

            return await _unitOfWork.Repository<Order>().GetAllAsync(orderRepositoryObject);
        }
    }
}