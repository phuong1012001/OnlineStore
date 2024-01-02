using AutoMapper;
using OnlineStore.BusinessLogic.Dtos.Product;
using OnlineStore.Cms.Models.Request.Product;
using OnlineStore.Cms.Models.Response.Product;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.Cms.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<ProductDto, ProductRes>()
                .ForMember(des => des.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.User.FristName + " " + src.User.LastName));

            CreateMap<ProductReq, ProductDto>();
            CreateMap<ProductDto, ProductEditRes>();
            CreateMap<ProductEditReq, ProductDto>();
        }
    }
}
