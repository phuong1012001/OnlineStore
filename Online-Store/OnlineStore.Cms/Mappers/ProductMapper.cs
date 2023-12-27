using AutoMapper;
using OnlineStore.BusinessLogic.Dtos.Auth;
using OnlineStore.BusinessLogic.Dtos.Category;
using OnlineStore.BusinessLogic.Dtos.Product;
using OnlineStore.Cms.Models.Response.Product;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.Cms.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            //Index
            CreateMap<Product, ProductDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<User, UserDto>();
            CreateMap<ProductDto, ProductRes>()
                .ForMember(des => des.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<UserDto, UserCreateRes>();
        }
    }
}
