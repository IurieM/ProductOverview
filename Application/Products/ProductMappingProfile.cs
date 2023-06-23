using AutoMapper;
using ProductOverview.Application.Products.Queries;
using ProductOverview.Domain.Products;

namespace ProductOverview.Application.Products;

internal class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}
