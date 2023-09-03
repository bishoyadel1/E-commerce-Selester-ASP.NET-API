using System.Collections;
using Domian.Entities;
using Domin.Entities;
using Infrastructue.Interfaces;
using Infrastructure.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SelesterDbContext _context;
        public IProductRepository<Product> Product { get; private set; }


        public IBasketRepository<CustomerBasket> Basket { get; private set; }

     
        public UnitOfWork(SelesterDbContext context , IConnectionMultiplexer redis)
        {
            _context = context;
            Product = new ProductRepository<Product>(context);
            Basket = new BasketRepository<CustomerBasket>(redis);

        }
        public IGenericRepository<T> GenericRepository<T>() where T : class  =>  new GenericRepository<T>(_context);
        

        public int complite()
        {
            try
            {
              return  _context.SaveChanges();
            }
            catch (ObjectDisposedException ex)
            {
                // Log error
                // Rethrow or throw new exception  
                return default;
            }
        }

        public void Dispose() => complite();

       
    }
}