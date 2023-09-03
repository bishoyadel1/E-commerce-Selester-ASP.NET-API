using Domian.Entities;
using Domian.Entities.OrderAggregate;
using Infrastructue.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        public IBasketRepository<CustomerBasket> Basket { get; private set; }
        public IUnitOfWork UnitOfWork { get; }

        public OrderService(IUnitOfWork  unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrderAsync(string basketId, string buyerEmail, ShippingAdderss adderss, int deleviryMethodId)
        {
            var basketitems = await UnitOfWork.Basket.GetBasketAcync(basketId) ;
            var OrderItems = new List<OrderItem>();
            foreach (var item in basketitems.BasketItems)
            {
                var product = await UnitOfWork.Product.GetByIdAsync(item.Id);
               var OrderItem = new OrderItem()
                {
                    PictureUrl = item.PictureUrl,
                    Price = product.Price,
                    Quntity = item.Quntity,
                   ProductName = product.Name,
                   ProducId = product.Id,
               };
                OrderItems.Add(OrderItem);
            }
            var delevirymethod = await UnitOfWork.GenericRepository<DeleviryMethod>().GetByIdAsync(deleviryMethodId);
            var subtotal = OrderItems.Sum(i=>i.Price*i.Quntity);
            var order = new Order(buyerEmail,adderss, delevirymethod,OrderItems,subtotal );
            await UnitOfWork.GenericRepository<Order>().AddAsync(order);
            var result = UnitOfWork.complite();
            if(result != 0 )
                await UnitOfWork.Basket.DeleteBasketAsync(basketId);
            return order;
            
        }

        
        public async Task<IReadOnlyList<Order>> GetUserOrderAsync(int Id ,string buyerEmail)
        {

            var spec = new OrderWithItemAndDeliveryMethodSpec(Id, buyerEmail);
            return await UnitOfWork.GenericRepository<Order>().ListAsync(spec);
        }
        public async Task<IReadOnlyList<Order>> GetUserOrdersAsync( string buyerEmail)
        {

            var spec = new OrderWithItemAndDeliveryMethodSpec (buyerEmail);
            return await UnitOfWork.GenericRepository<Order>().ListAsync(spec);
        }




        public Task<IReadOnlyList<DeleviryMethod>> GetDeleviryMethodAsync() 
            => UnitOfWork.GenericRepository<DeleviryMethod>().ListAllAsync();
  
    }
}
