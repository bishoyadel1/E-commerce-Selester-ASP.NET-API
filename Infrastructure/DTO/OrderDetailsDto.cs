using Domian.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class OrderDetailsDto
    {
        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } 
        public ShippingAdderss shippingAdderss { get; set; }

        public DeleviryMethod DeleviryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Total { get; set; }
    }
}
