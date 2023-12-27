using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Category;
using OnlineStore.BusinessLogic.Dtos.Product;
using OnlineStore.DataAccess.DbContexts;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.BusinessLogic.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProducts();
        Task<ProductDto> GetProduct(int id);
        Task<ProductResultDto> CreateProduct(ProductDto productDto);
        Task<List<ProductDto>> SearchProduct(string keyword);
        Task<ProductResultDto> AddCart(ProductInCartDto productInCartDto);
    }

    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly OnlineStoreDbContext _context;

        public ProductService(IMapper mapper, OnlineStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<ProductDto>> GetProducts()
        {
            var products = _context.Products
                .Include(c => c.Category)
                .Include(u => u.User)
                .ToList();

            var result = _mapper.Map<List<ProductDto>>(products);

            return result;
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var result = new ProductDto();

            var product = await _context.Products
                .Include (c => c.Category)
                .Include (u => u.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(product == null)
            {
                result.ErrorMessage = ErrorCodes.NotFoundProduct;
                return result;
            }

            result = _mapper.Map<ProductDto>(product);

            return result;
        }

        public async Task<ProductResultDto> CreateProduct(ProductDto productDto)
        {
            var result = new ProductResultDto();

            var category = await _context.Category
                .FirstOrDefaultAsync(x => x.Id == productDto.CategoryId);

            if (category == null)
            {
                result.ErrorMessage = ErrorCodes.NotFoundCategory;
                return result;
            }

            var product = _mapper.Map<Product>(productDto);
            product.CreatedBy = 2;

            _context.Category.Update(category);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<List<ProductDto>> SearchProduct(string keyword)
        {
            var products = new List<ProductDto>();

            var productDb = _context.Products
                .Include(c => c.Category)
                .Include(u => u.User)
                .Where(i => i.Category.Name!.Contains(keyword));

            products = _mapper.Map<List<ProductDto>>(productDb);

            return products;
        }

        public async Task<ProductResultDto> AddCart(ProductInCartDto productInCartDto)
        {
            var result = new ProductResultDto();

            var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Id == productInCartDto.UserId);

            if (user == null)
            {
                result.ErrorMessage = ErrorCodes.NotFoundUser;
                return result;
            }

            var product = await _context.Products
                    .FirstOrDefaultAsync(x => x.Id == productInCartDto.ProductId);

            if (product == null)
            {
                result.ErrorMessage = ErrorCodes.NotFoundProduct;
                return result;
            }

            var stock = await _context.Stocks
                    .FirstOrDefaultAsync(x => x.Id == productInCartDto.ProductId);

            if (stock == null)
            {
                result.ErrorMessage = ErrorCodes.NotFoundStock;
                return result;
            }

            if (productInCartDto.Quantity <= 0 || productInCartDto.Quantity > stock.Quantity)
            {
                result.ErrorMessage = ErrorCodes.InvalidQuantity;
                return result;
            }

            var cart = await _context.Carts
                .FirstOrDefaultAsync(x => x.CustomerId == productInCartDto.UserId);

            if (cart == null)
            {
                result.ErrorMessage = ErrorCodes.NotFoundCart;
                return result;
            }

            var cartDetailProduct = await _context.CartDetails
                    .FirstOrDefaultAsync(x => x.CartId == cart.Id
                        && x.ProductId == product.Id);

            if (cartDetailProduct != null)
            {
                cartDetailProduct.Quantity = productInCartDto.Quantity;
                _context.CartDetails.Update(cartDetailProduct);
            }
            else
            {
                var cartDetailEntity = new CartDetail
                {
                    Cart = cart,
                    ProductId = productInCartDto.ProductId,
                    Quantity = productInCartDto.Quantity,
                };

                _context.CartDetails.Add(cartDetailEntity);
            }

            await _context.SaveChangesAsync();

            result.Success = true;
            return result;
        }
    }
}
