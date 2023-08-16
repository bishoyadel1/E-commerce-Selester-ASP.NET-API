using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper
{
    public class Pagination <T> where T : class
    {
        public Pagination(int pageindex,int pagesize, IReadOnlyList<T> data)
        {
            Pageindex = pageindex;
            Pagesize = pagesize;
            Data = data;
        }

        public int Pageindex { get; }
        public int Pagesize { get; }
        public IReadOnlyList<T> Data { get; }
    }
}
