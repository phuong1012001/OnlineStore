using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Models.Response.Cart;
using OnlineStore.Api.Models.Responses.Base;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Cart;
using OnlineStore.BusinessLogic.Service;

namespace OnlineStore.Api.Controllers
{
    [Route("api/Cart")]
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        public CartController(
            ILogger<BaseController> logger,
            IMapper mapper,
            ICartService cartService)
            : base(logger, mapper)
        {
            _cartService = cartService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Cart(int userId)
        {
            var response = new ResultRes<CartRes>();

            try
            {
                var result = await _cartService.GetCart(userId);
                response.Result = Mapper.Map<CartRes>(result);
                response.Success = true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Cart failed: {ex}", ex);
                return InternalServerError(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> EditQuantity(int id, int cartId, int quantity)
        {
            var response = new ResultRes<CartRes>();

            try
            {
                var editCartDto = new EditCartDto
                {
                    ProductId = id,
                    CartId = cartId,
                    Quantity = quantity
                };

                var result = await _cartService.ChangeQuantity(editCartDto);

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

                response.Result = Mapper.Map<CartRes>(result);
                response.Success = true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Cart failed: {ex}", ex);
                return InternalServerError(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> Purchase(int userId)
        {
            var response = new ExecutionRes();

            try
            {
                var result = await _cartService.AddOrder(userId);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    switch(result.ErrorMessage)
                    {
                        case ErrorCodes.NotFoundCart:
                            response.Errors = SetError("Doesn't find cart.");
                            break;
                        case ErrorCodes.EmptyCart:
                            response.Errors = SetError("Empty cart.");
                            break;
                        case ErrorCodes.InvalidQuantity:
                            response.Errors = SetError("The quantity of the product is not enough.");
                            break;
                    }
                    return BadRequest(response);
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Purchase failed: {ex}", ex);
                return InternalServerError(response);
            }

            return Ok(response);
        }
    }
}
