using Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, ShippingAdderss shippingAdderss, DeleviryMethod deleviryMethod, IReadOnlyList<OrderItem> orderItems, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            this.shippingAdderss = shippingAdderss;
            DeleviryMethod = deleviryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public ShippingAdderss shippingAdderss  { get; set; }

        public DeleviryMethod DeleviryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems  { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Prnding; 
        public decimal GetTotal() => SubTotal + DeleviryMethod.Price;
    }
}
