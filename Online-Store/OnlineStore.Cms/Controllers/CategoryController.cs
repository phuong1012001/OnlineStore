using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Category;
using OnlineStore.BusinessLogic.Service;
using OnlineStore.Cms.Models.Request.Category;
using OnlineStore.Cms.Models.Response.Category;

namespace OnlineStore.Cms.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        protected IMapper Mapper { get; }

        public CategoryController(
            ILogger<CategoryController> logger,
            ICategoryService categoryService,
            IMapper mapper)
        {
            _logger = logger;
            _categoryService = categoryService;
            Mapper = mapper;
        }

        // GET: CategoryController
        public async Task<IActionResult> Index(string? searchString)
        {
            try
            {
                var result = await _categoryService.GetCategories(searchString);
                return View(Mapper.Map<CategoryRes[]>(result));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Create
        public IActionResult Create()
        {
            CategoryReq categoryReq = new CategoryReq();
            return PartialView("Create", categoryReq);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryReq categoryReq)
        {
            try
            {
                if (string.IsNullOrEmpty(categoryReq.Name)
                    || string.IsNullOrEmpty(categoryReq.Description)
                    || string.IsNullOrEmpty(categoryReq.Image))
                {
                    return BadRequest("No empty.");
                }

                await _categoryService.CreateCategory(Mapper.Map<CategoryDto>(categoryReq));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Update/5
        public async Task<IActionResult> Update(int id)
        {
            var result = await _categoryService.GetCategory(id); ;
            if (!string.IsNullOrEmpty(result.ErrorCode))
            {
                switch (result.ErrorCode)
                {
                    case ErrorCodes.CategoryNotFound:
                        return BadRequest("Doesn't find category.");
                }
            }

            var category = Mapper.Map<CategoryEditRes>(result.CategoryDto);

            return PartialView("Update", category);
        }

        // POST: CategoryController/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, CategoryEditReq category)
        {
            try
            {
                if (string.IsNullOrEmpty(category.Name)
                    || string.IsNullOrEmpty(category.Description)
                    || string.IsNullOrEmpty(category.Image))
                {
                    return BadRequest("No empty.");
                }

                var categoryEditReq = Mapper.Map<CategoryDto>(category);
                categoryEditReq.Id = id;

                var errorCode = await _categoryService.UpdateCategory(categoryEditReq);

                if (!string.IsNullOrWhiteSpace(errorCode))
                {
                    switch (errorCode)
                    {
                        case ErrorCodes.CategoryNotFound:
                            return BadRequest("Doesn't find category.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _categoryService.GetCategory(id);
                if (!string.IsNullOrWhiteSpace(result.ErrorCode))
                {
                    switch (result.ErrorCode)
                    {
                        case ErrorCodes.CategoryNotFound:
                            return BadRequest("Doesn't find category.");
                    }
                }

                return PartialView("Delete");
            }
            catch
            {
                return View();
            }
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var errorCode = await _categoryService.DeleteCategory(id);

                if (!string.IsNullOrWhiteSpace(errorCode))
                {
                    switch (errorCode)
                    {
                        case ErrorCodes.CategoryNotFound:
                            return BadRequest("Doesn't find category.");
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
