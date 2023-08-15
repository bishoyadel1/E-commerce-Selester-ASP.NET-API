using Domin.Entities;
using Infrastructue.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Infrastructure.Specifications;
using Infrastructure.DTO;
using AutoMapper;

namespace E_commerce_Selester_APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public IMapper Mapper { get; }
        public ProductController(IUnitOfWork unitOfWork , IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            Mapper = mapper;
        }

      

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> Get()
        {
            var spce = new ProductWithTypeAndBrand();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 32 // Set the maximum allowed depth as needed
            };
            var data = await _unitOfWork.Product.GetEntityWithSpec(spce);
            var model = Mapper.Map<Product, ProductDto>(data);

            var json = JsonSerializer.Serialize(model, options);
            return Content(json, "application/json");
        }


    }
}
