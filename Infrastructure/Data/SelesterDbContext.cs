using Domian.Entities.OrderAggregate;
using Domin.Entities;
using Domin.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure
{
    public class SelesterDbContext : IdentityDbContext<AppUserModel>
    {
        public SelesterDbContext(DbContextOptions<SelesterDbContext> options) : base(options) { }

        public DbSet<Product>  Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeleviryMethod> DeleviryMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Order>().OwnsOne(op => op.shippingAdderss, i => i.WithOwner());
            builder.Entity<Order>()
                 .Property(t => t.OrderStatus)
                                   .HasConversion(
                                            v => v.ToString(),
                                            v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));
            builder.Entity<Order>().HasMany(c => c.OrderItems).WithOne();
        }
               


    }

}
