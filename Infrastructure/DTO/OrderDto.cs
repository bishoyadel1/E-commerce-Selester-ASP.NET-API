using Domian.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class OrderDto
    {

        public string BasketId { get; set; }
        public int DeleviryMethodId { get; set; }
        public ShippingAdderss ShippingAdderss{ get; set; }

    }
}
