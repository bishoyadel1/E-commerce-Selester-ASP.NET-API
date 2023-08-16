using Core.Specifications;
using Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    public class ProductWithTypeAndBrand : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrand() {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        public ProductWithTypeAndBrand(ProductParam param) : base(x => (!param.BrandtId.HasValue|| x.ProductTypeId==param.TypeId)&&(!param.BrandtId.HasValue || x.ProductBrandId==param.BrandtId))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            ApplyPaging(param.MaxPageSize * (param.PageIndex - 1), param.MaxPageSize);
        }
    }
}
