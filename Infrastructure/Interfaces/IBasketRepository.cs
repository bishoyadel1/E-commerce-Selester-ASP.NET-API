using Domian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IBasketRepository<T> where T : class
    {
        Task<T> GetBasketAcync (string BasketId);
        Task<T> UpdateBasketAsync (CustomerBasket basket);
        Task<T> DeleteBasketAsync (string BasketId);    

    }
}
