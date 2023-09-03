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
    public class ProductWithTypeAndBrandCount : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandCount() {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        public ProductWithTypeAndBrandCount(ProductParam param) : base(x => (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search.ToLower()))&&(!param.TypeId.HasValue|| x.ProductTypeId==param.TypeId)&&(!param.BrandtId.HasValue || x.ProductBrandId==param.BrandtId))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);

            if (!string.IsNullOrEmpty(param.Sort))
            {
                switch (param.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }
    }
}
