using System.Collections;
using Domin.Entities;
using Infrastructue.Interfaces;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SelesterDbContext _context;
        public IProductRepository<Product> Product { get; private set; }

        public UnitOfWork(SelesterDbContext context)
        {
            _context = context;
            Product = new ProductRepository<Product>(context);


        }

        public int complite() => _context.SaveChanges();

        public void Dispose() => complite();


    }
}