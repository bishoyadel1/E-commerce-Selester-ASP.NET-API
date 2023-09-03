using Domin.Entities;

namespace Domian.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public string ProductName { get; set; }
        public int ProducId { get; set; } 
        public int Quntity { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
    }
}