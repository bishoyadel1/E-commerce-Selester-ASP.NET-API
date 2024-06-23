using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Reports
{
    public class SellerProfitsReportDto
    {
        public int SellerId { get; set; }
        public int MDate { get; set; }
        public string SellerName { get; set; }
        public int? ProductCount { get; set; }
        public decimal? ProfitCount { get; set; }
    }
}
