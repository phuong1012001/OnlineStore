using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Models.Responses.Auth;
using OnlineStore.Api.Models.Responses.Base;
using OnlineStore.Api.Models.Responses.Product;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Product;
using OnlineStore.BusinessLogic.Services;

namespace OnlineStore.Api.Controllers
{
    [Route("api/Product")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(
            ILogger<BaseController> logger,
            IMapper mapper,
            IProductService productService)
            : base(logger, mapper)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(ResultRes<List<ProductRes>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Products()
        {
            var response = new ResultRes<List<ProductRes>>();

            try
            {
                var result = await _productService.GetProducts();

                response.Result = Mapper.Map<List<ProductRes>>(result);
                response.Success = true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Products failed: {ex}", ex);
                return InternalServerError(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultRes<ProductDetailRes>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var response = new ResultRes<ProductDetailRes>();

            try
            {
                var result = await _productService.GetProduct(id);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    switch (result.ErrorMessage)
                    {
                        case ErrorCodes.NotFoundProduct:
                            response.Errors = SetError("Don't find product.");
                            break;
                        case ErrorCodes.NotFoundStock:
                            response.Errors = SetError("Don't find stock.");
                            break;
                    }

                    return BadRequest(response);
                }

                response.Result = Mapper.Map<ProductDetailRes>(result);
                response.Success = true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Get product failed: {ex}", ex);
                return InternalServerError(response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("search/{keyword}")]
        [ProducesResponseType(typeof(ResultRes<List<ProductRes>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchProducts(string keyword)
        {
            var response = new ResultRes<List<ProductRes>>();

            try
            {
                var result = await _productService.SearchProduct(keyword);

                response.Result = Mapper.Map<List<ProductRes>>(result);
                response.Success = true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Products failed: {ex}", ex);
                return InternalServerError(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("{id}/add-cart")]
        public async Task<IActionResult> AddProductToCart(int id, int quantity, int userId)
        {
            var response = new ExecutionRes(); 
            try
            {
                var productInCart = new ProductInCartDto
                {
                    ProductId = id,
                    Quantity = quantity,
                    UserId = userId
                };

                var result = await _productService.AddCart(productInCart);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    switch (result.ErrorMessage)
                    {
                        case ErrorCodes.NotFoundUser:
                            response.Errors = SetError("Don't find user.");
                            break;
                        case ErrorCodes.NotFoundProduct:
                            response.Errors = SetError("Don't find product.");
                            break;
                        case ErrorCodes.NotFoundStock:
                            response.Errors = SetError("Don't find stock.");
                            break;
                        case ErrorCodes.NotFoundCart:
                            response.Errors = SetError("Don't find cart.");
                            break;
                        case ErrorCodes.InvalidQuantity:
                            response.Errors = SetError("Quantity ...");
                            break;
                    }

                    return BadRequest(response);
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Products failed: {ex}", ex);
                return InternalServerError(response);
            }
            return Ok(response);
        }
    }
}
