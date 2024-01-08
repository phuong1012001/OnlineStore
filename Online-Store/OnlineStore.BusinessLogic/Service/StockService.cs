using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Stock;
using OnlineStore.DataAccess.DbContexts;
using OnlineStore.DataAccess.Entities;
using OnlineStore.DataAccess.Enums;

namespace OnlineStore.BusinessLogic.Service
{
    public interface IStockService
    {
        Task<StockDto[]> GetStocks(string? SearchString);
        Task<string> CreateStockEvent(StockEventDto stockEventDto);
    }

    public class StockService : IStockService
    {
        private readonly IMapper _mapper;
        private readonly OnlineStoreDbContext _context;

        public StockService(IMapper mapper, OnlineStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<StockDto[]> GetStocks(string? SearchString)
        {
            var stock = from s in _context.Stocks
                        join p in _context.Products on s.ProductId equals p.Id
                        join c in _context.Categories on p.CategoryId equals c.Id
                        select new StockDto
                        {
                            Id = s.Id,
                            ProductId = s.ProductId,
                            ProductName = p.Name,
                            CategoryName = c.Name,
                            Quantity = s.Quantity,

                        };
            if (!string.IsNullOrEmpty(SearchString))
            {
                stock = stock
                    .Where(s => s.ProductName!.Contains(SearchString)
                    || s.CategoryName!.Contains(SearchString));
            }

            return stock.ToArray();  
        }

        public async Task<string> CreateStockEvent(StockEventDto stockEventDto)
        {
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(i => i.Id == stockEventDto.StockId);

            if (stock == null)
            {
                return ErrorCodes.StockNotFound;
            }

            if (stockEventDto.Quantity <= 0)
            {
                return ErrorCodes.InvalidQuantity;
            }

            if (stockEventDto.Type == Types.In)
            {
                stock.Quantity += stockEventDto.Quantity;
            }

            if (stockEventDto.Type == Types.Out)
            {
                if (stockEventDto.Quantity > stock.Quantity)
                {
                    return ErrorCodes.InvalidQuantity;
                }

                stock.Quantity -= stockEventDto.Quantity;
            }

            var stockEvent = _mapper.Map<StockEvent>(stockEventDto);
            stockEvent.CreatedAt = DateTime.Now;

            _context.Stocks.Update(stock);
            _context.StockEvents.Add(stockEvent);
            await _context.SaveChangesAsync();

            return string.Empty;
        }
    }
}
