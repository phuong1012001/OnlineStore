using AutoMapper;
using OnlineStore.Api.Models.Responses.Product;
using OnlineStore.BusinessLogic.Dtos.Product;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.Api.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<Product, ProductDto>();

            CreateMap<ProductDto, ProductRes>();

            CreateMap<CategoryDto, CategoryRes>();

            CreateMap<ProductDto, ProductDetailRes>();
        }
    }
}
