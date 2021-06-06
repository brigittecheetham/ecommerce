using System.Collections.Generic;

namespace core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
            
        }

        public CustomerBasket(string id)
        {
            Id = id;

        }
        public string Id { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntendId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}