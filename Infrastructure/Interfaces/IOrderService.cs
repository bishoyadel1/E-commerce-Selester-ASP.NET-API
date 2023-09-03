using Domian.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IOrderService
    {
         Task<Order> CreateOrderAsync(string basketId, string buyerEmail, ShippingAdderss adderss, int deleviryMethodId);
            Task <IReadOnlyList<Order>>  GetUserOrdersAsync(string buyerEmail);
        Task <IReadOnlyList<DeleviryMethod>> GetDeleviryMethodAsync();
        Task<IReadOnlyList<Order>> GetUserOrderAsync(int Id, string buyerEmail);
    } 
}
