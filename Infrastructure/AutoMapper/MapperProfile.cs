using AutoMapper;
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
            }
        
    }
}
