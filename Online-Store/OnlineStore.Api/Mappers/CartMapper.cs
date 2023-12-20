using AutoMapper;
using OnlineStore.Api.Models.Response.Cart;
using OnlineStore.BusinessLogic.Dtos.Cart;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.Api.Mappers
{
    public class CartMapper : Profile
    {
        public CartMapper()
        {
            CreateMap<Cart, CartDto>();

            CreateMap<CartDetail, CartDetailDto>();

            CreateMap<Product, ProductCartDto>();

            CreateMap<CartDto, CartRes>();

            CreateMap<CartDetailDto, CartDetailRes>();

            CreateMap<ProductCartDto, CartDetailRes>();

            CreateMap<Cart, Order>();

            CreateMap<CartDetail, OrderDetail>();
        }
    }
}
