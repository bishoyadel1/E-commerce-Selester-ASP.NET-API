using AutoMapper;
using Domian.Entities;
using Infrastructue.Interfaces;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace E_commerce_Selester_APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        public IMapper Mapper { get; }
        private IUnitOfWork _unitOfWork { get; }
        public BasketController(IMapper mapper , IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult<BasketDto>> Get(string id)
        {
            var data = await _unitOfWork.Basket.GetBasketAcync(id);
            return Ok(data ?? new CustomerBasket(id)); 

        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> Post(BasketDto model)
        {
            if(ModelState.IsValid)
            {
                var data = Mapper.Map<BasketDto , CustomerBasket>(model);
                data.Id = model.Id;
                var created = await _unitOfWork.Basket.UpdateBasketAsync(data);
                if (created != null)
                    return null;
                return Ok(created);
            }
            return null;
        }
        [HttpDelete ]
        public async Task<ActionResult<BasketDto>> Delete(string id)
        {
            if (id != null)
            {
                var delete = await _unitOfWork.Basket.DeleteBasketAsync(id);
                if(delete != null)
                    return Ok(delete);
                return null;
            }
            return null;
        }

    }
}
