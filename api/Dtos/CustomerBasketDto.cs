using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> BasketItems { get; set; } 
    }
}