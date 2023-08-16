using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    public class ProductParam
    {
        public int MaxPageSize { get; set; } = 50;
        public int PageSize { get; set; }


        public int? BrandtId { get; set; }
        public int? TypeId { get; set; }
        public int PageIndex { get; set; } = 1;

    }
}
