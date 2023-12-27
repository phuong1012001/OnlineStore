using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Models.Requests.Auth;
using OnlineStore.Api.Models.Responses.Auth;
using OnlineStore.Api.Models.Responses.Base;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Auth;
using OnlineStore.BusinessLogic.Services;

namespace OnlineStore.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(
            ILogger<BaseController> logger,
            IMapper mapper,
            IAuthService authService)
            : base(logger, mapper)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(ResultRes<LoginRes>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginReq request)
        {
            var response = new ResultRes<LoginRes>();
            
            try
            {
                if (string.IsNullOrEmpty(request.Email)
                    || string.IsNullOrEmpty(request.Password))
                {
                    response.Errors = SetError("No empty.");
                    return BadRequest(response);
                }

                var result = await _authService.LoginUser(Mapper.Map<LoginDto>(request));

                if (!string.IsNullOrEmpty(result.ErrorCode))
                {
                    switch(result.ErrorCode)
                    {
                        case ErrorCodes.NotFoundUser:
                            response.Errors = SetError("Email or password invalid.");
                            break;
                    }
                }

                response.Result = Mapper.Map<LoginRes>(result);
                response.Success = true; 
            }
            catch (Exception ex)
            {
                Logger.LogError("Login failed: {ex}", ex);
                return InternalServerError(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(ResultRes<RegisterRes>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterReq request)
        {
            var response = new ResultRes<RegisterRes>();

            try
            {
                if (string.IsNullOrEmpty(request.Email)
                    || string.IsNullOrEmpty(request.Password)
                    || string.IsNullOrEmpty(request.FristName)
                    || string.IsNullOrEmpty(request.LastName)
                    || string.IsNullOrEmpty(request.PhoneNumber))
                {
                    response.Errors = SetError("No empty.");
                    return BadRequest(response);
                }

                var result = await _authService.RegisterUser(Mapper.Map<UserDto>(request));

                if (!string.IsNullOrEmpty(result.ErrorCode))
                {
                    switch (result.ErrorCode)
                    {
                        case ErrorCodes.AccountAlreadyExists:
                            response.Errors = SetError("Account already exists.");
                            break;
                    }
                }

                response.Result = Mapper.Map<RegisterRes>(result);
                response.Success = true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Register failed: {ex}", ex);
                return InternalServerError(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("password")]
        [ProducesResponseType(typeof(ResultRes<ChangePasswordRes>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword(ChangePasswordReq request)
        {
            var response = new ResultRes<ChangePasswordRes>();

            try
            {
                if (string.IsNullOrEmpty(request.Email)
                    || string.IsNullOrEmpty(request.PasswordOld)
                    || string.IsNullOrEmpty(request.PasswordNew))
                {
                    response.Errors = SetError("No empty");
                    return BadRequest(response);
                }

                var result = await _authService.ChangePasswordUser(Mapper.Map<ChangePasswordDto>(request));

                if (!string.IsNullOrEmpty(result.ErrorCode))
                {
                    switch(result.ErrorCode)
                    {
                        case ErrorCodes.IncorrectEmail:
                            response.Errors = SetError("Incorrect Email");
                            break;
                        case ErrorCodes.IncorrectPassword:
                            response.Errors = SetError("Incorrect Password");
                            break;
                    }

                    return BadRequest(response);
                }

                response.Result = Mapper.Map<ChangePasswordRes>(result);
                response.Success = true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Change password failed: {ex}", ex);
                return InternalServerError(response);
            }

            return Ok(response);
        }
    }
}
