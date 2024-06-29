using Entity.Models;

namespace API.Dto
{
    public class BasketDto
    {

        public string ClientId { get; set; }

        public ICollection<BasketItemsDto> Items { get; set; }

        public string? PaymentIntendId { get; set; }
        public string? ClientSecrit { get; set; }
    }
}
