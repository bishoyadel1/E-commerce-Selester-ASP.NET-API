using Core.DTOs.Reports;
using Core.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AdminReportRepository : IAdminReportRepository
    {
        private readonly ECommerceDBContext context;

        public AdminReportRepository(ECommerceDBContext _context)
        {
            context = _context;
        }



        public async Task<ICollection<SellerProfitsReportDto>> GetProfitReportByDate(DateTime startDate, DateTime endDate)
        {
            List<SellerProfitsReportDto> ProfitreportDtos = new List<SellerProfitsReportDto>();

            var profitSeller = await context.SellerProfits.Where(O => O.Date >= startDate && O.Date <= endDate).OrderByDescending(o =>o.ProfitCount).ToListAsync();

            foreach (var profit in profitSeller)
            {
                ProfitreportDtos.Add(new SellerProfitsReportDto
                {
                    SellerId= profit.UserId,
                    SellerName = context.Users.FirstOrDefault(u => u.Id == profit.UserId).FullName,
                    MDate = profit.Date.Date.Month,
                    ProfitCount = profit.ProfitCount,
                    ProductCount = profit.ProductCount
                });
            }

            return ProfitreportDtos;
        }


        public async Task<ICollection<AdminReportDto>> GetReportByDate(DateTime startDate, DateTime endDate)
        {

            List<AdminReportDto> OrderreportDtos = new List<AdminReportDto>();
  

            var orders = context.Orders.Include(O => O.OrderDetails)
                .Where(O => O.Date >= startDate && O.Date <= endDate).OrderByDescending(o =>o.Date);
       

            foreach (var order in orders)
            {
                OrderreportDtos.Add(new AdminReportDto
                {
                    OrderId = order.Id,
                    Date = order.Date,
                    Status = order.Status,
                    TotalPrice = order.TotalPrice,
                    ProductsCount = order.OrderDetails.Sum(OD => OD.Quantity)
                });
            }
   
       

            return OrderreportDtos;
        }
    }
}
