using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Product;
using OnlineStore.DataAccess.DbContexts;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.BusinessLogic.Services
{
    public interface IProductService
    {
        Task<ProductDto[]> GetProducts(string? searchString);
        Task<(string ErrorCode, ProductDto ProductDto)> GetProduct(int id);
        Task<string> CreateProduct(ProductDto productDto);
        Task<string> UpdateProduct(ProductDto productDto);
        Task<string> DeleteProduct(int id);
        //Task<ProductResultDto> AddCart(ProductInCartDto productInCartDto);
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

        public async Task<ProductDto[]> GetProducts(string? SearchString)
        {
            var productQuery = _context.Products
                .Include(c => c.Category)
                .Include(u => u.User);

            if (!string.IsNullOrEmpty(SearchString))
            {
                var productSearchQuery = productQuery
                .Where(i => i.Category.Name.Contains(SearchString)
                    || i.Name.Contains(SearchString));

                return _mapper.Map<ProductDto[]>(productSearchQuery.ToArray());
            }

            return _mapper.Map<ProductDto[]>(productQuery.ToArray());
        }

        public async Task<(string ErrorCode, ProductDto ProductDto)> GetProduct(int id)
        {
            var productDto = new ProductDto();

            var product = await _context.Products
                .Include (c => c.Category)
                .Include (u => u.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            if(product == null)
            {
                return (ErrorCodes.ProductNotFound, productDto);
            }

            productDto = _mapper.Map<ProductDto>(product);

            return (null, productDto);
        }

        public async Task<string> CreateProduct(ProductDto productDto)
        {
            var category = await _context.Categorys
                .FirstOrDefaultAsync(x => x.Id == productDto.CategoryId);
            if (category == null)
            {
                return ErrorCodes.CategoryNotFound;
            }

            var product = _mapper.Map<Product>(productDto);
            product.CreatedBy = 2;
            product.CreatedAt = DateTime.Now;

            var stock = new Stock
            {
                Product = product,
                Quantity = productDto.Quantity,
            };

            _context.Products.Add(product);
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return string.Empty;
        }

        public async Task<string> UpdateProduct(ProductDto productDto)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == productDto.Id);
            if (product == null)
            {
                return ErrorCodes.ProductNotFound;
            }

            var category = await _context.Categorys
                .FirstOrDefaultAsync(x => x.Id == productDto.CategoryId);
            if (category == null)
            {
                return ErrorCodes.CategoryNotFound;
            }


            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Thumbnail = productDto.Thumbnail;
            product.UnitPrice = productDto.UnitPrice;
            product.CategoryId = productDto.CategoryId;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return string.Empty;
        }

        public async Task<string> DeleteProduct(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return ErrorCodes.ProductNotFound;
            }

            product.isDeleted = true;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return string.Empty;
        }

        //public async Task<ProductResultDto> AddCart(ProductInCartDto productInCartDto)
        //{
        //    var result = new ProductResultDto();

        //    var user = await _context.Users
        //            .FirstOrDefaultAsync(x => x.Id == productInCartDto.UserId);

        //    if (user == null)
        //    {
        //        result.ErrorMessage = ErrorCodes.NotFoundUser;
        //        return result;
        //    }

        //    var product = await _context.Products
        //            .FirstOrDefaultAsync(x => x.Id == productInCartDto.ProductId);

        //    if (product == null)
        //    {
        //        result.ErrorMessage = ErrorCodes.ProductNotFound;
        //        return result;
        //    }

        //    var stock = await _context.Stocks
        //            .FirstOrDefaultAsync(x => x.Id == productInCartDto.ProductId);

        //    if (stock == null)
        //    {
        //        result.ErrorMessage = ErrorCodes.NotFoundStock;
        //        return result;
        //    }

        //    if (productInCartDto.Quantity <= 0 || productInCartDto.Quantity > stock.Quantity)
        //    {
        //        result.ErrorMessage = ErrorCodes.InvalidQuantity;
        //        return result;
        //    }

        //    var cart = await _context.Carts
        //        .FirstOrDefaultAsync(x => x.CustomerId == productInCartDto.UserId);

        //    if (cart == null)
        //    {
        //        result.ErrorMessage = ErrorCodes.NotFoundCart;
        //        return result;
        //    }

        //    var cartDetailProduct = await _context.CartDetails
        //            .FirstOrDefaultAsync(x => x.CartId == cart.Id
        //                && x.ProductId == product.Id);

        //    if (cartDetailProduct != null)
        //    {
        //        cartDetailProduct.Quantity = productInCartDto.Quantity;
        //        _context.CartDetails.Update(cartDetailProduct);
        //    }
        //    else
        //    {
        //        var cartDetailEntity = new CartDetail
        //        {
        //            Cart = cart,
        //            ProductId = productInCartDto.ProductId,
        //            Quantity = productInCartDto.Quantity,
        //        };

        //        _context.CartDetails.Add(cartDetailEntity);
        //    }

        //    await _context.SaveChangesAsync();

        //    result.Success = true;
        //    return result;
        //}
    }
}
