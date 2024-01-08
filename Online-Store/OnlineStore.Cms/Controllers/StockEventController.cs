using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Service;
using OnlineStore.Cms.Models.Response.Stock;

namespace OnlineStore.Cms.Controllers
{
    public class StockEventController : Controller
    {
        private readonly ILogger<StockEventController> _logger;
        private readonly IStockService _stockService;
        protected IMapper Mapper { get; }

        public StockEventController(
            ILogger<StockEventController> logger,
            IStockService stockService,
            IMapper mapper)
        {
            _logger = logger;
            _stockService = stockService;
            Mapper = mapper;
        }

        // GET: StockEventController
        public async Task<IActionResult> IndexAsync(string? SearchString)
        {
            try
            {
                var result = await _stockService.GetStockEvents(SearchString);
                return View(Mapper.Map<StockEventRes[]>(result));
            }
            catch
            {
                return View();
            }
        }
    }
}
