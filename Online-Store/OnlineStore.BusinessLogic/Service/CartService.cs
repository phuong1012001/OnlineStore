using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Cart;
using OnlineStore.DataAccess.DbContexts;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.BusinessLogic.Service
{
    public interface ICartService
    {
        Task<CartDto> GetCart(int userId);

        Task<CartDto> ChangeQuantity(EditCartDto editCartDto);

        Task<CartResultDto> AddOrder(int userId);
    }

    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly OnlineStoreDbContext _context;

        public CartService(IMapper mapper, OnlineStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CartDto> GetCart(int userId)
        {
            var cartDto = new CartDto();

            var cart = await _context.Carts
                .Include(cd => cd.CartDetails)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.CustomerId == userId);
            
            if (cart == null)
            {
                cartDto.ErrorMessage = ErrorCodes.NotFoundCart;
                return cartDto;
            }

            cartDto = _mapper.Map<CartDto>(cart);

            if (cart.CartDetails == null)
            {
                cartDto.Total = (float)0;
            } else
            {
                cartDto.Total = cart.CartDetails.Sum(x => x.Quantity * x.Product.UnitPrice);
            }

            return cartDto;
        }

        public async Task<CartDto> ChangeQuantity(EditCartDto editCartDto)
        {
            var cartDto = new CartDto();

            var productCart = _context.CartDetails
                .FirstOrDefault(x => x.CartId == editCartDto.CartId
                    && x.ProductId == editCartDto.ProductId);

            if (productCart == null)
            {
                cartDto.ErrorMessage = ErrorCodes.NotFoundProduct;
                return cartDto;
            }

            var stock = await _context.Stocks
                    .FirstOrDefaultAsync(x => x.Id == editCartDto.ProductId);

            if (stock == null)
            {
                cartDto.ErrorMessage = ErrorCodes.NotFoundStock;
                return cartDto;
            }

            if (editCartDto.Quantity > stock.Quantity || editCartDto.Quantity < 0)
            {
                cartDto.ErrorMessage = ErrorCodes.InvalidQuantity;
                return cartDto;
            }

            if (editCartDto.Quantity == 0)
            {
                _context.CartDetails.Remove(productCart);
            }
            else
            {
                productCart.Quantity = editCartDto.Quantity;
            }

            _context.CartDetails.Update(productCart);
            await _context.SaveChangesAsync();

            var cart = await _context.Carts
                .Include(cd => cd.CartDetails)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.Id == editCartDto.CartId);

            if (cart == null)
            {
                cartDto.ErrorMessage = ErrorCodes.NotFoundCart;
                return cartDto;
            }

            cartDto = _mapper.Map<CartDto>(cart);

            if (cart.CartDetails == null)
            {
                cartDto.Total = (float)0;
            }
            else
            {
                cartDto.Total = cart.CartDetails.Sum(x => x.Quantity * x.Product.UnitPrice);
            }

            return cartDto;
        }

        public async Task<CartResultDto> AddOrder(int userId)
        {
            var result = new CartResultDto();

            var cart = await _context.Carts
                .Include(cd => cd.CartDetails)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.CustomerId == userId);

            if (cart == null)
            {
                result.ErrorMessage = ErrorCodes.NotFoundCart;
                return result;
            }

            if (cart.CartDetails == null)
            {
                result.ErrorMessage = ErrorCodes.EmptyCart;
                return result;
            }

            var orderEntity = _mapper.Map<Order>(cart);
            float total = 0;

            foreach (var productCart in cart.CartDetails)
            {
                var stock = await _context.Stocks
                    .FirstOrDefaultAsync(x => x.Id == productCart.ProductId);

                if (productCart.Quantity <= 0 || productCart.Quantity > stock.Quantity)
                {
                    result.ErrorMessage = ErrorCodes.InvalidQuantity;
                    return result;
                }

                var orderDetailEntity = _mapper.Map<OrderDetail>(productCart);
                orderDetailEntity.Order = orderEntity;
                orderDetailEntity.UnitPrice = productCart.Product.UnitPrice;

                stock.Quantity -= productCart.Quantity;

                total += productCart.Quantity * productCart.Product.UnitPrice;

                _context.OrderDetail.Add(orderDetailEntity);
                _context.Stocks.Update(stock);
            }

            orderEntity.CreatedAt = DateTime.UtcNow;
            orderEntity.Total = total;

            _context.Order.Add(orderEntity);
            _context.CartDetails.RemoveRange(cart.CartDetails);
            await _context.SaveChangesAsync();

            result.Success = true;

            return result;
        }
    }
}
