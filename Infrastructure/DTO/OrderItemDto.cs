using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class OrderItemDto
    {
        public string ProductName { get; set; }
        public int ProducId { get; set; }
        public int Quntity { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
    }
}
