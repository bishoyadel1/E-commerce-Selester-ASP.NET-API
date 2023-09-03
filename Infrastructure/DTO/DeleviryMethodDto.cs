using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class DeleviryMethodDto
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeleviryTime { get; set; }
        public decimal Price { get; set; }
    }
}
