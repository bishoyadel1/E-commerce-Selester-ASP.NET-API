using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper
{
    public class Pagination <T> where T : class
    {
        public Pagination(int totalCount,int pageindex,int pagesize, IReadOnlyList<T> data)
        { 
            TotalCount = totalCount;
            Pageindex = pageindex;
            Pagesize = pagesize;
            Data = data;
        }

        public int TotalCount { get; }
        public int Pageindex { get; }
        public int Pagesize { get; }
        public IReadOnlyList<T> Data { get; }
    }
}
