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
    }
}