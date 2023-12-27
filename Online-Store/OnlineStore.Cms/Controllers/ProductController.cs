using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Services;
using OnlineStore.Cms.Models.Response.Product;

namespace OnlineStore.Cms.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        protected IMapper Mapper { get; }

        public ProductController(
            ILogger<ProductController> logger,
            IProductService productService,
            IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            Mapper = mapper;
        }

        // GET: ProductController
        public async Task<IActionResult> IndexAsync(string? searchString)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    //var resultSearch = await _productService.GetSearch(searchString);
                    //return View(Mapper.Map<List<CategoryRes>>(resultSearch));
                }

                var result = await _productService.GetProducts();
                return View(Mapper.Map<List<ProductRes>>(result));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
