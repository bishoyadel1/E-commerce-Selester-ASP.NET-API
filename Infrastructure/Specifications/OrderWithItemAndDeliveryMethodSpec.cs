using Core.Specifications;
using Domian.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    public class OrderWithItemAndDeliveryMethodSpec : BaseSpecification<Order>
    {
        public OrderWithItemAndDeliveryMethodSpec(string BuyerEmail) : base(i=>i.BuyerEmail== BuyerEmail)
        {
            AddInclude(i => i.DeleviryMethod);
            AddInclude(o => o.OrderItems);
        }
        public OrderWithItemAndDeliveryMethodSpec(int Id ,string BuyerEmail) : base(i=>(i.BuyerEmail == BuyerEmail)&&(i.Id==Id))
        {
            AddInclude(i => i.DeleviryMethod);
            AddInclude(o => o.OrderItems);
        }
    }
}
