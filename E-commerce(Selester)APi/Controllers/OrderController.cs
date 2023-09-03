using AutoMapper;
using Domian.Entities.OrderAggregate;
using Infrastructure.DTO;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using Order = Domian.Entities.OrderAggregate.Order;

namespace E_commerce_Selester_APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
       
        public OrderController(IOrderService orderService , IMapper mapper)
        {
            _OrderService = orderService;
            _Mapper = mapper;
        }

        public IOrderService _OrderService { get; }
        public IMapper _Mapper { get; }

        [HttpPost]
        public async Task<ActionResult<Order>> Post(OrderDto model)
        {
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await  _OrderService.CreateOrderAsync(model.BasketId,UserEmail, model.ShippingAdderss, model.DeleviryMethodId);
            if(order is not null)
            {
                return Ok(order);
            }
            return BadRequest();

        }
        [HttpGet("UserOrders")]
        public async Task<ActionResult<IReadOnlyList<OrderDetailsDto>>> UserOrders() 
        {
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders =await _OrderService.GetUserOrdersAsync(UserEmail);
            var data = _Mapper.Map<OrderDetailsDto>(orders);
            if (orders is not null)
            {
                return Ok(data);
            }
            return BadRequest();
        }
        [HttpGet("UserOrder")]
        public async Task<ActionResult<IReadOnlyList<OrderDetailsDto>>> UserOrder( int id)
        {
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _OrderService.GetUserOrderAsync(id,UserEmail);
            var data = _Mapper.Map<OrderDetailsDto>(orders);
            if (orders is not null)
            {
                return Ok(data);
            }
            return BadRequest();
        }




    }
}
