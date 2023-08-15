using Domin.Entities;

namespace Infrastructue.Interfaces
{
    public interface IProductRepository <T> : IGenericRepository<T> where T : class
    {
        Task<T> GetProductByIdAsync(int id);


    }
}