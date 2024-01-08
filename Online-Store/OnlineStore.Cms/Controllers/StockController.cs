using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Stock;
using OnlineStore.BusinessLogic.Service;
using OnlineStore.Cms.Models.Request.Stock;
using OnlineStore.Cms.Models.Response.Stock;

namespace OnlineStore.Cms.Controllers
{
    public class StockController : Controller
    {
        private readonly ILogger<StockController> _logger;
        private readonly IStockService _stockService;
        protected IMapper Mapper { get; }

        public StockController(
            ILogger<StockController> logger,
            IStockService stockService,
            IMapper mapper)
        {
            _logger = logger;
            _stockService = stockService;
            Mapper = mapper;
        }

        // GET: StoclController
        public async Task<IActionResult> Index(string? SearchString)
        {
            try
            {
                var result = await _stockService.GetStocks(SearchString);
                return View(Mapper.Map<StockRes[]>(result));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoclController/Create/id
        public IActionResult Create(int id)
        {
            StockEventReq request = new StockEventReq();
            return PartialView("Create", request);
        }

        // POST: StoclController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, StockEventReq request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Reason))
                {
                    return BadRequest("No empty.");
                }

                var stockEventDto = Mapper.Map<StockEventDto>(request);
                stockEventDto.StockId = id;

                var errorCode = await _stockService.CreateStockEvent(stockEventDto);
                if (!string.IsNullOrWhiteSpace(errorCode))
                {
                    switch (errorCode)
                    {
                        case ErrorCodes.StockNotFound:
                            return BadRequest("Doesn't find stock.");
                        case ErrorCodes.InvalidQuantity:
                            return BadRequest("Invalid quantity.");
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
