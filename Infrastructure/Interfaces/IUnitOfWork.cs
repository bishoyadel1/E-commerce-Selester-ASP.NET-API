using Domin.Entities;

namespace Infrastructue.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
      IProductRepository<Product> Product { get; }
        int complite();
    }
}