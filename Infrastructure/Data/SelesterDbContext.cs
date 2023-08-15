
using Domin.Entities;
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
      


    }

}
