using Domian.Entities;
using Domin.Entities;
using Infrastructure.Interfaces;

namespace Infrastructue.Interfaces
{
    public interface IUnitOfWork : IDisposable 
    {
        IProductRepository<Product> Product { get; }
        IBasketRepository<CustomerBasket> Basket { get; }
        IGenericRepository<T> GenericRepository<T>() where T : class;


        int complite();
    }
}