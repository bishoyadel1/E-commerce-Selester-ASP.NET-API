using Domin.Entities;
using Infrastructue.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository<T> : GenericRepository<T> , IProductRepository<T> where T : class
    {
        private readonly SelesterDbContext _context;

        public ProductRepository(SelesterDbContext context) : base(context) { _context = context; }
        
        
        public async Task<T> GetProductByIdAsync(int id)
        {
            return _context.Set<T>().Find(id);
        }


  


    }
}