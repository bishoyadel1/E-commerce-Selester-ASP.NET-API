using AutoMapper;
using Domian.Entities;
using Domian.Entities.OrderAggregate;
using Domin.Entities;
using Infrastructure.DTO;
using Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
      
            public MapperProfile()
            {
                CreateMap<Product, ProductDto>()
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ProductUrlReslover>())
                    .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.ProductType.Name))
                    .ForMember(dest => dest.ProductBrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
                    .ReverseMap();
            CreateMap<ProductBrand, BrandDto>().ReverseMap();
            CreateMap<ProductType, TypeDto>().ReverseMap();
            CreateMap<BasketDto, CustomerBasket>().ForMember(d=>d.Id , op=>op.MapFrom(src=>src.Id));
            CreateMap<CustomerBasket, BasketDto>();   
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<Order,OrderDetailsDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ForMember(d => d.PictureUrl, o => o.MapFrom<OrderDetailsUrlReslover>());
            CreateMap<ShippingAdderss, ShippingAdderssDto>().ReverseMap();
            CreateMap<DeleviryMethod, DeleviryMethodDto>().ReverseMap();
        }
        
    }
}
