using Domin.Entities;
using Infrastructue.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Infrastructure.Specifications;
using Infrastructure.DTO;
using AutoMapper;
using Infrastructure.Helper;

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
        public async Task<ActionResult<ProductDto>> Get([FromQuery] int id)
        {
            var spec = new ProductWithTypeAndBrand(id);
            var product = await _unitOfWork.Product.GetEntityWithSpec(spec);
            return Ok(Mapper.Map<ProductDto>(product));
       
        }
        [HttpGet("GetProducts")]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductParam param)
        {
            var spac = new ProductWithTypeAndBrand(param);
            var model = await _unitOfWork.Product.ListAsync(spac);
            var data  = Mapper.Map <IReadOnlyList<ProductDto>>(model);
            var count = await _unitOfWork.Product.CountAsync(new ProductWithTypeAndBrandCount(param));
     
            return Ok(new Pagination<ProductDto>(count,param.PageIndex, param.MaxPageSize, data));
            
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> Brands()
        {

            var model = await _unitOfWork.GenericRepository<ProductBrand>().ListAllAsync();
            var BrandDto = Mapper.Map<IReadOnlyList<BrandDto>>(model);
            return Ok(BrandDto);

        }
        [HttpGet("ProductType")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> ProductType()
        {

            var model = await _unitOfWork.GenericRepository<ProductType>().ListAllAsync();
            var BrandDto = Mapper.Map<IReadOnlyList<TypeDto>>(model);
            return Ok(BrandDto);

        }

        internal record struct NewStruct(object Item1, object Item2)
    {
        public static implicit operator (object, object)(NewStruct value)
        {
            return (value.Item1, value.Item2);
        }

        public static implicit operator NewStruct((object, object) value)
        {
            return new NewStruct(value.Item1, value.Item2);
        }
    }
}
}
