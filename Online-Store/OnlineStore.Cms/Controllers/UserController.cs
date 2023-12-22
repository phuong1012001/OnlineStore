using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Auth;
using OnlineStore.BusinessLogic.Services;
using OnlineStore.Cms.Models.Request.User;
using OnlineStore.Cms.Models.Response.User;

namespace OnlineStore.Cms.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IAuthService _authService;
        protected IMapper Mapper { get; }

        public UserController(
            ILogger<UserController> logger,
            IAuthService authService,
            IMapper mapper)
        {
            _logger = logger;
            _authService = authService;
            Mapper = mapper;
        }

        // GET: User
        public async Task<IActionResult> Index(string? searchString)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    var resultSearch = await _authService.GetSearch(searchString);
                    return View(Mapper.Map<List<UserRes>>(resultSearch));
                }

                var result = await _authService.GetUsers();
                return View(Mapper.Map<List<UserRes>>(result));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Create()
        {
            UserReq userReq = new UserReq();
            return PartialView("Create", userReq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserReq userReq)
        {
            try
            {
                if (string.IsNullOrEmpty(userReq.Email)
                    || string.IsNullOrEmpty(userReq.Password)
                    || string.IsNullOrEmpty(userReq.FristName)
                    || string.IsNullOrEmpty(userReq.LastName)
                    || string.IsNullOrEmpty(userReq.PhoneNumber)
                    || string.IsNullOrEmpty(userReq.Civilianld))
                {
                    return BadRequest("No empty.");
                }

                var result = await _authService.RegisterUser(Mapper.Map<UserDto>(userReq));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _authService.GetUser(id); ;

            if (!string.IsNullOrEmpty(result.ErrorCode))
            {
                switch (result.ErrorCode)
                {
                    case ErrorCodes.NotFoundUser:
                        return BadRequest("Doesn't find user.");
                }
            }

            var user = Mapper.Map<UserEditRes>(result);

            return PartialView("Edit", user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserEditReq user)
        {
            try
            {
                var userEditReq = Mapper.Map<UserDto>(user);
                userEditReq.Id = id;
                var result = await _authService.SaveUser(userEditReq);

                if (!string.IsNullOrEmpty(result.ErrorCode))
                {
                    switch(result.ErrorCode)
                    {
                        case ErrorCodes.NotFoundUser:
                            return BadRequest("Doesn't find user.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var result = await _authService.GetUser(id);

                if (!string.IsNullOrEmpty(result.ErrorCode))
                {
                    switch (result.ErrorCode)
                    {
                        case ErrorCodes.NotFoundUser:
                            return BadRequest("Doesn't find user.");
                    }
                }

                return PartialView("Delete");
            }
            catch
            {
                return View();
            }
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var result = await _authService.DeleteUser(id);

                if (!string.IsNullOrEmpty(result.ErrorCode))
                {
                    switch (result.ErrorCode)
                    {
                        case ErrorCodes.NotFoundUser:
                            return BadRequest("Doesn't find user.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
