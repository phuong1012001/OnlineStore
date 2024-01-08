using OnlineStore.DataAccess.Enums;

namespace OnlineStore.BusinessLogic.Dtos.Stock
{
    public class StockEventDto
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public string? ProductName { get; set; }

        public string? CategoryName { get; set; }

        public Types Type { get; set; }

        public string? Reason { get; set; }

        public int Quantity { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
