using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntendId { get; set; }        
        public List<BasketItemDto> BasketItems { get; set; } 
        public decimal ShippingPrice { get; set; }
    }
}