using Application.Dtos.ProductCategory.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class ProductCategoryMappers : Profile
    {

        public ProductCategoryMappers()
        {
            CreateMap<ProductCategory, ProductCategorySummaryResponse>();
        }
    }
}
