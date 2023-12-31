﻿using Core.Specifications;
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
        public ProductWithTypeAndBrand(int id) : base(x=>x.Id==id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
        public ProductWithTypeAndBrand(ProductParam param) : base(x => (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search.ToLower()))&&(!param.TypeId.HasValue|| x.ProductTypeId==param.TypeId)&&(!param.BrandtId.HasValue || x.ProductBrandId==param.BrandtId))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            ApplyPaging(param.MaxPageSize * (param.PageIndex - 1), param.MaxPageSize);

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
