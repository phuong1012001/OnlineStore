using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Product;
using OnlineStore.BusinessLogic.Service;
using OnlineStore.BusinessLogic.Services;
using OnlineStore.Cms.Models.Request.Product;
using OnlineStore.Cms.Models.Response.Product;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.Cms.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        protected IMapper Mapper { get; }

        public ProductController(
            ILogger<ProductController> logger,
            IProductService productService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            Mapper = mapper;
        }

        // GET: ProductController
        public async Task<IActionResult> Index(string? SearchString)
        {
            try
            {
                var result = await _productService.GetProducts(SearchString);
                return View(Mapper.Map<ProductRes[]>(result));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Create
        public async Task<IActionResult> Create()
        {
            var categorys = await _categoryService.GetCategories();
            ViewData["CategoryList"] = new SelectList(categorys,
                nameof(Category.Id),
                nameof(Category.Name));
            ProductReq productReq = new ProductReq();

            return PartialView("Create", productReq);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductReq request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name)
                    || string.IsNullOrEmpty(request.Description)
                    || string.IsNullOrEmpty(request.Thumbnail))
                {
                    return BadRequest("No empty.");
                }

                var errorCode = await _productService.CreateProduct(Mapper.Map<ProductDto>(request));
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

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Update(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _productService.GetProduct(id); ;

            if (!string.IsNullOrWhiteSpace(result.ErrorCode))
            {
                switch (result.ErrorCode)
                {
                    case ErrorCodes.ProductNotFound:
                        return BadRequest("Doesn't find product.");
                }
            }

            var product = Mapper.Map<ProductEditRes>(result.ProductDto);

            var categorys = await _categoryService.GetCategories();
            ViewData["CategoryList"] = new SelectList(categorys,
                nameof(Category.Id),
                nameof(Category.Name));

            return PartialView("Update", product);
        }

        // POST: ProductController/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, ProductEditReq request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name)
                    || string.IsNullOrEmpty(request.Description)
                    || string.IsNullOrEmpty(request.Thumbnail))
                {
                    return BadRequest("No empty.");
                }

                var product = Mapper.Map<ProductDto>(request);
                product.Id = id;

                var errorCode = await _productService.UpdateProduct(product);
                if (!string.IsNullOrWhiteSpace(errorCode))
                {
                    switch (errorCode)
                    {
                        case ErrorCodes.CategoryNotFound:
                            return BadRequest("Doesn't find category.");
                        case ErrorCodes.ProductNotFound:
                            return BadRequest("Doesn't find product.");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _productService.GetProduct(id);
                if (!string.IsNullOrWhiteSpace(result.ErrorCode))
                {
                    switch (result.ErrorCode)
                    {
                        case ErrorCodes.ProductNotFound:
                            return BadRequest("Doesn't find product.");
                    }
                }

                return PartialView("Delete");
            }
            catch
            {
                return View();
            }
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var errorCode = await _productService.DeleteProduct(id);
                if (!string.IsNullOrWhiteSpace(errorCode))
                {
                    switch (errorCode)
                    {
                        case ErrorCodes.ProductNotFound:
                            return BadRequest("Doesn't find product.");
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
