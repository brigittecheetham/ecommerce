using api.Dtos;
using AutoMapper;
using core.Entities;

namespace api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductResponseDto>()
            .ForMember(p => p.ProductBrand, options => options.MapFrom(source => source.ProductBrand.Name))
            .ForMember(p => p.ProductType, options => options.MapFrom(source => source.ProductType.Name))
            .ForMember(p => p.PictureUrl, options => options.MapFrom<ProductUrlResolver>() );
        }
    }
}