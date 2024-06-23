using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Reports
{
    public class reportDtos
    {
        public List<AdminReportDto> AdminReports { get; set; }
        public List<SellerProfitsReportDto> SellerProfits { get; set; }

        public reportDtos()
        {
            AdminReports = new List<AdminReportDto>();
            SellerProfits = new List<SellerProfitsReportDto>();
        }
    }
}
