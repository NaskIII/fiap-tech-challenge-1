using Application.Dtos.ProductDtos.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class ProductMappers : Profile
    {

        public ProductMappers()
        {
            CreateMap<Product, ProductSummaryResponse>()
                .ForMember(x => x.ProductName, z => z.MapFrom(src => src.ProductName.Value));
        }
    }
}
