using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Models.Responses.Base;

namespace OnlineStore.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected ILogger<BaseController> Logger { get; }

        public BaseController(
            ILogger<BaseController> logger,
            IMapper mapper)
        {
            Mapper = mapper;
            Logger = logger;
        }

        [NonAction]
        protected IActionResult InternalServerError(object? data)
        {
            if (data is ExecutionRes apiResult)
            {
                apiResult.Errors = SetError(string.IsNullOrEmpty(apiResult.Errors?.Description)
                    ? "Something went wrong. Please try again later"
                    : apiResult.Errors.Description);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, data);
        }

        [NonAction]
        protected ErrorRes SetError(string description)
        {
            return new ErrorRes
            {
                Description = description
            };
        }
    }
}
